using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using _4;

namespace _4.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;

        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, TvChannelContext db)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                DbInitializer.Initialize(db);
                context.Session.SetString("starting", "Yes");
            }

            return _next.Invoke(context);
        }
    }
}
