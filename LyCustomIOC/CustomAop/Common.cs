using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyCustomIOC.CustomAop
{
    public class Common
    {
        public virtual void MethodInterceptor()
        {
            Console.WriteLine("this is Interceptor");
        }

        public void MethodNoInterceptor()
        {
            Console.WriteLine("this is without Interceptor");
        }
    }
}
