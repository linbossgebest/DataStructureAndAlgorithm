using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniDIFrameWork
{
    /// <summary>
    /// 自定义Cat容器的扩展方法
    /// </summary>
    public static class CatExtensions
    {
        public static Cat Register(this Cat cat, Type from, Type to, Lifetime lifetime)
        {
            Func<Cat, Type[], object> factory = (_, arguments) => Create(_,to,arguments);
            cat.Regeister(new ServiceRegistry(from, lifetime, factory));
            return cat;
        }

        public static Cat Register<TFrom, TTo>(this Cat cat, Lifetime lifetime) where TTo : TFrom
        {
            return cat.Register(typeof(TFrom), typeof(TTo), lifetime);
        }

        public static Cat Register(this Cat cat, Type serviceType, object instance)
        {
            Func<Cat, Type[], object> factory = (_, arguments) => instance;
            cat.Regeister(new ServiceRegistry(serviceType, Lifetime.Root, factory));
            return cat;
        }

        public static Cat Register<TService>(this Cat cat, TService instance)
        {
            Func<Cat, Type[], object> factory = (_, arguments) => instance;
            cat.Regeister(new ServiceRegistry(typeof(TService), Lifetime.Root, factory));
            return cat;
        }
        public static Cat Register(this Cat cat, Type serviceType,
        Func<Cat, object> factory, Lifetime lifetime)
        {
            cat.Regeister(new ServiceRegistry(serviceType, lifetime, (_,arguments)=>factory(_)));
            return cat;
        }
        public static Cat Register<TService>(this Cat cat,
           Func<Cat, TService> factory, Lifetime lifetime)
        {
            cat.Regeister(new ServiceRegistry(typeof(TService), lifetime, (_, arguments) => factory(_)));
            return cat;
        }

        /// <summary>
        /// 程序集注入
        /// </summary>
        /// <param name="cat">容器对象</param>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        public static Cat Register(this Cat cat, Assembly assembly)
        {
            var typeAttributes = from type in assembly.GetExportedTypes()
                                 let attribute = type.GetCustomAttribute<MapToAttribute>()
                                 where attribute != null
                                 select new { ServiceType = type, Attribute = attribute };
            foreach (var item in typeAttributes)
            {
                cat.Register(item.Attribute.ServiceType, item.ServiceType, item.Attribute.Lifetime);
            }
            return cat;
        }

        public static Cat CreateChild(this Cat cat)
        {
            return new Cat(cat);
        }

        public static T GetService<T>(this Cat cat)
        {
            return (T)cat.GetService(typeof(T));
        }

        public static IEnumerable<T> GetServices<T>(this Cat cat)
        {
            return cat.GetService<IEnumerable<T>>();
        }

        private static object Create(Cat cat, Type type, Type[] genericArguments)
        {
            //判断是否有泛型参数
            if (genericArguments.Length > 0)
            {
                //替代由当前泛型类型定义的类型参数组成的类型数组的元素，并返回表示结果构造类型的 Type 对象  ：Foobar<,>=>Foobar<IFoo,IBar>
                type = type.MakeGenericType(genericArguments);
            }
            //获取所有的构造函数
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new InvalidOperationException($"Cannot create the instance of {type} which does not have an public constructor.");
            }
            //找到自定义特性为InjectionAttribute的构造函数
            var constructor = constructors.FirstOrDefault(f => f.GetCustomAttributes(false).OfType<InjectionAttribute>().Any());
            //如果没有找到自定义特性为InjectionAttribute的构造函数，则使用所有构造函数数组中的第一个构造函数
            constructor ??= constructors.First();
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(type);
            }
            var arguments = new object[parameters.Length];
            for (int index = 0; index < arguments.Length; index++)
            {
                arguments[index] = cat.GetService(parameters[index].ParameterType);
            }
            return constructor.Invoke(arguments);
        }
    }
}
