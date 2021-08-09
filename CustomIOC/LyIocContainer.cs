using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CustomIOC
{
    public sealed class LyIocContainer
    {
       private Dictionary<string, Type> typeDic = new Dictionary<string, Type>();

        public void Register<TFrom,TTo>()
        {
            string typeName = typeof(TFrom).FullName;
            if (!typeDic.ContainsKey(typeName))
                typeDic.Add(typeName,typeof(TTo));
        }

        public TFrom Resolve<TFrom>()
        {
            return (TFrom)ResolveObject(typeof(TFrom));
        }

        private object ResolveObject(Type abstractType)
        {
            string key = abstractType.FullName;
            Type type = typeDic[key];
            var ctor = type.GetConstructors()[0];//获取构造函数

            List<object> paramList = new List<object>();//参数列表
            foreach (var param in ctor.GetParameters())//构造函数参数
            {
                Type paramType = param.ParameterType;
                object paramInstance = ResolveObject(paramType);
                paramList.Add(paramInstance);
            }

            object oInstance = Activator.CreateInstance(type, paramList);
            return oInstance;

        }
    }
}
