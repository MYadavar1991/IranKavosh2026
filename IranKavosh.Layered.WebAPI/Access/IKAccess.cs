using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IranKavosh.Layered.WebAPI.Access
{
    public class IKAccess : Attribute, IAsyncActionFilter
    {
      //  private readonly string _requiredRole;

        public IKAccess()
        {
          //  _requiredRole = requiredRole;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                string controllerName = descriptor.ControllerName;
                string actionName = descriptor.ActionName;
                string httpMethod = context.HttpContext.Request.Method;

                // مثال: لاگ‌گیری یا بررسی دسترسی
                Console.WriteLine($"درخواست {httpMethod} به {controllerName}/{actionName}");
            }

            await next();
        }
    }
}
