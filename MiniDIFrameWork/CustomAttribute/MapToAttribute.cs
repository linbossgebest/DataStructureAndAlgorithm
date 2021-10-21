using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDIFrameWork
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true)]
    public sealed class MapToAttribute:Attribute
    {
        public Type ServiceType { get; }

        public Lifetime Lifetime { get; }

        public MapToAttribute(Type serviceType, Lifetime lifetime)
        {
            ServiceType= serviceType;
            Lifetime = lifetime;
        }
    }
}
