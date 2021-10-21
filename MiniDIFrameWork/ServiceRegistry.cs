using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDIFrameWork
{
    /// <summary>
    /// 服务注册类
    /// </summary>
    public class ServiceRegistry
    {
        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get;}

        /// <summary>
        /// 生命周期
        /// </summary>
        public Lifetime Lifetime { get; }

        /// <summary>
        /// 创建服务实例工厂
        /// </summary>
        public Func<Cat,Type[],object> Factory { get;  }

        internal ServiceRegistry Next { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="lifetime"></param>
        /// <param name="factory"></param>
        public ServiceRegistry(Type serviceType, Lifetime lifetime, Func<Cat, Type[], object> factory)
        {
            this.ServiceType = serviceType;
            this.Lifetime = lifetime;
            this.Factory = factory;
        }

        internal IEnumerable<ServiceRegistry> AsEnumerable()
        {
            var list = new List<ServiceRegistry>();
            for (var self = this; self != null; self=self.Next)
            {
                list.Add(self);
            }
            return list;
        }

    }
}
