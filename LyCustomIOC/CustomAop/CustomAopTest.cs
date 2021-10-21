using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyCustomIOC.CustomAop
{
    public class CustomAopTest
    {
        public static void Show()
        {
            ProxyGenerator generator = new ProxyGenerator();
            CustomInterceptor interceptor = new CustomInterceptor();
            Common test = generator.CreateClassProxy<Common>(interceptor);

            Console.WriteLine($"当前的类型为：{test.GetType()},父类型为：{test.GetType().BaseType}");
            Console.WriteLine();
            test.MethodInterceptor();
            Console.WriteLine();
            test.MethodNoInterceptor();
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
