using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace PipeLine
{
    class Program
    {
        static void Main(string[] args)
        {
            static RequestDelegate Middleware1(RequestDelegate next) => async context =>
            {
                await context.Response.WriteAsync("Hello");
                await next(context);
            };
            static RequestDelegate Middleware2(RequestDelegate next) => async context =>
            {
                await context.Response.WriteAsync(" World!");
                await next(context);
            };
            static RequestDelegate Middleware3(RequestDelegate next)
            {
               return context => test1(context);
                //RequestDelegate requestDelegate = test1;
                //return requestDelegate;
                //return async context =>
                //{
                //    await context.Response.WriteAsync(" World!");
                //};
            }
            // static RequestDelegate Middleware3(RequestDelegate next)
            // {
            //     return  test1(context);
            //}
            static RequestDelegate test(HttpContext context)
            {
                return (context) => test1(context);
            }
            static async Task test1(HttpContext context)
            {
                await context.Response.WriteAsync("hello1188");
            }

            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder.Configure(app => app
                .Use(Middleware1)
                .Use(Middleware2)
                .Use(Middleware3)))
            .Build()
            .Run(); 
        }

        

    }
}
