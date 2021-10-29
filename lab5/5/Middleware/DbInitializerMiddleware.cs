using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using _5.Models;

namespace _5.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            // инициализация базы данных 
            _next = next;
        }
        public Task Invoke(HttpContext context)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                //DbUserInitializer.Initialize(context).Wait();
                DbInitializer.Initialize(context.RequestServices.GetRequiredService<PublishingLabContext>());
                context.Session.SetString("starting", "Yes");
            }

            // Вызов следующего делегата / компонента middleware в конвейере
            return _next.Invoke(context);
        }
    }
}
