using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Security.Application;
using System.Reflection;

namespace MM.Suppliers.API.Web.Filters
{
    public class SanitizeInputAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ActionArguments != null && actionContext.ActionArguments.Count == 1)
            {
                var requestParam = actionContext.ActionArguments.First();
                var properties = requestParam.Value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => x.CanRead && x.CanWrite && x.PropertyType == typeof(string) && x.GetGetMethod(true).IsPublic && x.GetSetMethod(true).IsPublic);
                foreach (var propertyInfo in properties)
                {
                    propertyInfo.SetValue(requestParam.Value, Encoder.HtmlEncode(propertyInfo.GetValue(requestParam.Value) as string));
                }
            }
        }
    }
}
