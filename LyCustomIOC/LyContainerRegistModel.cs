using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyCustomIOC
{
    public class LyContainerRegistModel
    {
        //HttpContext;

        public Type TargetType { get; set; }

        public LifeTimeType LifeTime { get; set; }

        public object SingletonInstance { get; set; }

    }

    public enum LifeTimeType
    {
        Transient,//瞬时
        Scoped,//作用域
        Singleton//单例
    }
}
