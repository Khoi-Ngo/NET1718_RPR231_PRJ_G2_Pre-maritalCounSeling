﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Pre_maritalCounSeling.BAL.Auth
{
    public class AuthenticateOnlyAttribute : Attribute, IAuthorizationFilter
    {
        //Authentication filter only
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
            }
        }
    }

}
