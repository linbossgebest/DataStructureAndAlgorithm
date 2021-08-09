using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyCustomCommon
{
    /// <summary>
    /// 有别名的参数或属性 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter|AttributeTargets.Property)]
    public class LyNickNameParamAttribute : Attribute
    {
        public string NickName { get;private set; }
        public LyNickNameParamAttribute(string nickName)
        {
            this.NickName = nickName;
        }
    }
}
