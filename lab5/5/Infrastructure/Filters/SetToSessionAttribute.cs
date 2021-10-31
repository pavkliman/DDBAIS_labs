using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _5.Infrastructure.Filters
{
    public class SetToSessionAttribute : Attribute, IActionFilter
    {
        private string _name;//имя ключа
        public SetToSessionAttribute(string name)
        {
            _name = name;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (context.ModelState.Count > 0)
            {
                foreach (var item in context.ModelState)
                {
                    dict.Add(item.Key, item.Value.AttemptedValue);
                }
                context.HttpContext.Session.Set(_name, dict);
            }

        }
    }
}
