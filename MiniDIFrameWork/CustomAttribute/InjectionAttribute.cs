using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDIFrameWork
{
    /// <summary>
    /// 自定义构造函数依赖注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionAttribute : Attribute 
    {
    }
}
