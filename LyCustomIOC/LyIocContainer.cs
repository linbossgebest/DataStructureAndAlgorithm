using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using LyCustomCommon;

namespace LyCustomIOC
{
    public sealed class LyIocContainer:ILyIocContainer
    {
        private Dictionary<string, LyContainerRegistModel> typeDic = new Dictionary<string, LyContainerRegistModel>();
        private Dictionary<string, object[]> paramListDic = new Dictionary<string, object[]>();
        private Dictionary<string, object> scopeDic = new Dictionary<string, object>();

        public LyIocContainer() { }

        private LyIocContainer(Dictionary<string, LyContainerRegistModel> typeDic, Dictionary<string, object[]> paramListDic, Dictionary<string, object> scopeDic)
        {
            this.typeDic = typeDic;
            this.paramListDic = paramListDic;
            this.scopeDic = scopeDic;
        }

        public ILyIocContainer CreateChildContainer()
        {
            return new LyIocContainer(this.typeDic, this.paramListDic, new Dictionary<string, object>());
        }

        /// <summary>
        /// 注入（请使用命名参数）
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="nickName"></param>
        /// <param name="paramList"></param>
        /// <param name="lifeTime"></param>
        public void Register<TFrom, TTo>(string nickName = null, object[] paramList = null, LifeTimeType lifeTime= LifeTimeType.Transient) where TTo : TFrom
        {
            string typeName = GetKey(typeof(TFrom), nickName);
            //typeDic.Add(typeName, typeof(TTo));
            typeDic.Add(typeName, new LyContainerRegistModel()
            {
                TargetType = typeof(TTo),
                LifeTime = lifeTime
            });
            if (paramList != null && paramList.Length > 0)
                paramListDic.Add(typeName, paramList);
        }

        public TFrom Resolve<TFrom>(string nickName = null)
        {
            return (TFrom)ResolveObject(typeof(TFrom), nickName);
        }

        private string GetKey(Type type, string nickName) => $"{type.FullName}@{nickName}";

        /// <summary>
        /// 获取别名
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private string GetNickName(ICustomAttributeProvider provider)
        {
            if (provider.IsDefined(typeof(LyNickNameParamAttribute), true))
            {
                var attribute = (LyNickNameParamAttribute)provider.GetCustomAttributes(typeof(LyNickNameParamAttribute), true)[0];
                return attribute.NickName;
            }

            return null;
        }

        private object ResolveObject(Type abstractType, string nickName = null)
        {
            string key = GetKey(abstractType, nickName);
            LyContainerRegistModel registModel = typeDic[key];

            switch (registModel.LifeTime)
            {
                case LifeTimeType.Transient:
                    break;
                case LifeTimeType.Scoped:
                    if (scopeDic.ContainsKey(key))
                        return scopeDic[key];
                    else
                        break;
                case LifeTimeType.Singleton:
                    if (registModel.SingletonInstance == null)
                        break;
                    else
                        return registModel.SingletonInstance;
                default:
                    break;
            }

            Type type = registModel.TargetType;

            #region 构造函数注入

            var ctor = type.GetConstructors().FirstOrDefault(f => f.IsDefined(typeof(LyConstructorAttribute), true));//根据特性获取构造函数
            if (ctor == null)//如果没有标记特性的构造函数，则使用参数最多的构造函数
                ctor = type.GetConstructors().OrderByDescending(f => f.GetParameters().Length).First();

            List<object> paramList = new List<object>();//参数列表
            object[] constantParamList = paramListDic.ContainsKey(key) ? paramListDic[key] : null;
            int paramIndex = 0;
            foreach (ParameterInfo param in ctor.GetParameters())//构造函数参数
            {
                if (param.IsDefined(typeof(LyConstantParamAttribute), true))//判断是否是常量参数
                {
                    paramList.Add(constantParamList[paramIndex++]);
                }
                else
                {
                    Type paramType = param.ParameterType;
                    string paramNickName = GetNickName(param);
                    object paramInstance = ResolveObject(paramType, paramNickName);
                    paramList.Add(paramInstance);
                }
            }

            #endregion

            object oInstance = Activator.CreateInstance(type, paramList);

            #region 属性注入

            foreach (PropertyInfo prop in type.GetProperties().Where(f => f.IsDefined(typeof(LyPropertyAttribute), true)))
            {
                Type propType = prop.PropertyType;
                string propNickName = GetNickName(prop);
                object propInstance = ResolveObject(propType, propNickName);
                prop.SetValue(oInstance, propInstance);
            }

            #endregion

            #region 方法注入

            foreach (MethodInfo methodInfo in type.GetMethods().Where(f => f.IsDefined(typeof(LyMethodAttribute), true)))
            {
                List<object> methodParamList = new List<object>();//需要注入方法的参数列表
                foreach (ParameterInfo param in methodInfo.GetParameters())
                {
                    Type paramType = param.ParameterType;
                    string paramNickName = GetNickName(param);
                    object paramInstance = ResolveObject(paramType, paramNickName);
                    methodParamList.Add(paramInstance);
                }
                methodInfo.Invoke(oInstance, methodParamList.ToArray());
            }

            #endregion

            switch (registModel.LifeTime)
            {
                case LifeTimeType.Transient:
                    break;
                case LifeTimeType.Scoped:
                    scopeDic[key] = oInstance;
                    break;
                case LifeTimeType.Singleton:
                    registModel.SingletonInstance = oInstance;
                    break;
                default:
                    break;
            }

            return oInstance;

        }
    }
}
