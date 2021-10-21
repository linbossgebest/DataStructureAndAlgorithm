using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDIFrameWork
{
    /// <summary>
    /// 依赖注入容器Cat
    /// </summary>
    public class Cat : IServiceProvider, IDisposable
    {
        internal readonly Cat _root;//根容器
        /// <summary>
        /// 服务注册列表
        /// </summary>
        internal readonly ConcurrentDictionary<Type, ServiceRegistry> _registries;
        /// <summary>
        /// 非Transient服务实例列表
        /// </summary>
        private readonly ConcurrentDictionary<Key, object> _services;
        /// <summary>
        /// 待释放服务实例列表
        /// </summary>
        private readonly ConcurrentBag<IDisposable> _disposables;
        private volatile bool _disposed;
        
        public Cat()
        {
            _root = this;
            _registries = new ConcurrentDictionary<Type, ServiceRegistry>();
            _services = new ConcurrentDictionary<Key, object>();
            _disposables = new ConcurrentBag<IDisposable>();
        }
        internal Cat(Cat parent)
        {
            _root = parent._root;
            _registries = new ConcurrentDictionary<Type, ServiceRegistry>();
            _services = new ConcurrentDictionary<Key, object>();
            _disposables = new ConcurrentBag<IDisposable>();
        }

        private void EnsureNotDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Cat));
            }
        }

        /// <summary>
        /// 容器注册服务类
        /// </summary>
        /// <param name="registry"></param>
        /// <returns></returns>
        public Cat Regeister(ServiceRegistry registry)
        {
            EnsureNotDisposed();
            //服务注册列表是否存在该类型的注册服务
            if (_registries.TryGetValue(registry.ServiceType, out var existing))
            {
                _registries[registry.ServiceType] = registry;
                registry.Next = existing;
            }
            else
            {
                _registries[registry.ServiceType] = registry;
            }
            return this;
        }

        public void Dispose()
        {
            _disposed = true;
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
            _services.Clear();
        }

        public object GetService(Type serviceType)
        {
            EnsureNotDisposed();

            if (serviceType == typeof(Cat) || serviceType == typeof(IServiceProvider))
            {
                return this;
            }

            ServiceRegistry registry;
            //IEnumerable<T>
            if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var elementType = serviceType.GetGenericArguments()[0];
                if (!_registries.TryGetValue(elementType, out registry))//服务注册列表是否存在该类型的服务注册
                {
                    return Array.CreateInstance(elementType, 0);
                }

                var registries = registry.AsEnumerable();
                var services = registries.Select(f => GetServicCore(f, Type.EmptyTypes)).ToArray();
                Array array = Array.CreateInstance(elementType, services.Length);
                services.CopyTo(array, 0);
                return array;
            }
            //Generic
            if (serviceType.IsGenericType && !_registries.ContainsKey(serviceType))
            {
                //获取泛型类型的开发类型: List<T> => List<>
                var definition = serviceType.GetGenericTypeDefinition();
                return _registries.TryGetValue(definition, out registry) ? GetServicCore(registry, new Type[0]) : null;
            }
            //Normal
            return _registries.TryGetValue(serviceType, out registry) ? GetServicCore(registry, new Type[0]) : null;
        }

        private object GetServicCore(ServiceRegistry registry, Type[] genericArguments)
        {
            var key=new Key(registry, genericArguments);
            var serviceType = registry.ServiceType;

            switch (registry.Lifetime)
            {
                case Lifetime.Root:return GetOrCreate(_root._services, _root._disposables);//单列生命周期下，从根容器获取
                case Lifetime.Self:return GetOrCreate(_services, _disposables);//scope生命周期下，从当前容器获取
                default: //瞬时生命周期，每次创建新的服务实例
                    {
                        var service = registry.Factory(this, genericArguments);
                        if (service is IDisposable disposable)
                        {
                            _disposables.Add(disposable);
                        }
                        return service;                  
                    }
            }

            object GetOrCreate(ConcurrentDictionary<Key,object> services,ConcurrentBag<IDisposable> disposables)
            {
                //先判断服务实例中有没有对应的service
                if (services.TryGetValue(key, out var service))
                {
                    return service;
                }
                //如果没有找到，则通过实例工厂创建
                service = registry.Factory(this, genericArguments);
                //将实例加入到字典中
                services[key] = service;
                //如果该服务实例实现了IDisposable接口，则加入到待释放服务列表中，以便在容器释放时执行dispose()
                if (service is IDisposable disposable)
                {
                    disposables.Add(disposable);
                }
                return service;
            }
        }
    }
}
