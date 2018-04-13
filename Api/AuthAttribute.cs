using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = filterContext.HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                filterContext.Result = new RedirectResult("/signin");
            }
        }
    }
}