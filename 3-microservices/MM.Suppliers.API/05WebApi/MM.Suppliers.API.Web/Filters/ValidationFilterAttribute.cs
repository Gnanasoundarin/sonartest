using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MM.Base.Core.ApiModels;
using MM.Base.Core.Models;
using System.Net;

namespace MM.Suppliers.API.Web.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorMsgs = context.ModelState.Values
                                    .SelectMany(v => v.Errors).Select(u => u.ErrorMessage);

                context.Result = new ObjectResult(new BaseResponseModel<BaseModel>().OnError(errorMsgs))
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}