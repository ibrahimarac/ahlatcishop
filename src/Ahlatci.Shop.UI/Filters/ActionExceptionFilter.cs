using Ahlatci.Shop.UI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Ahlatci.Shop.UI.Filters
{
    public class ActionExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var tempDataDictionaryFactory = (ITempDataDictionaryFactory)context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory));
            var tempData = tempDataDictionaryFactory.GetTempData(context.HttpContext);

            if(context.Exception is UnauthenticatedException unauthenticatedException)
            {
                tempData["error"] = unauthenticatedException.Message;
                context.Result = new RedirectToActionResult("SignIn", "Login", new { Area = "Admin" });
            }
            else if(context.Exception is UnauthorizedException unauthorizedException)
            {
                tempData["error"] = unauthorizedException.Message;
                context.Result = new RedirectToActionResult("SignIn", "Login", new { Area = "Admin" });
            }
        }
    }
}
