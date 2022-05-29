using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebPanel.Filters
{
    public class CustomAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
                return;

            var user = context.HttpContext.User;
            if (user == null)
                return;

            if (user.Identity.Name == null)
                return;


            if (user.Identity.Name == "passenger1")
                context.Result = new ForbidResult();

            return;
        }
    }
}
