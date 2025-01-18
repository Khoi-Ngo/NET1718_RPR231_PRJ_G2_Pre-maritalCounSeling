using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pre_maritalCounSeling.BAL.Auth
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string role;

        public PermissionAuthorizeAttribute(string role)
        {
            this.role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (context.HttpContext.User.Claims.Any(c => c.Type == "RoleName" && c.Value == role))
                {
                    return;
                }
                else
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
            context.Result = new StatusCodeResult(401);
        }
    }
}
