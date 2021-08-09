using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyCustomCommon
{
    /// <summary>
    /// 常量参数特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class LyConstantParamAttribute:Attribute
    {
    }
}
