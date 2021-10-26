using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;

namespace CoreTest
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .UseStartup<Startup>()
                .ConfigureServices(svcs => svcs.AddSingleton<IFoo, Foo>()))
            .Build()
            .Run();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.AddSingleton<IBar, Bar>();
        public void Configure(IApplicationBuilder app, IFoo foo, IBar bar)
        {
            Console.WriteLine(foo != null);
            Console.WriteLine(bar != null);
        }
    }

    public interface IFoo { }
    public class Foo : IFoo { }
    public interface IBar { }
    public class Bar : IBar { }
}
