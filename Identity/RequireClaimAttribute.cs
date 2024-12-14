﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography.X509Certificates;
namespace Azurenet.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimName;
        private readonly string _claimValue;
        public RequireClaimAttribute(string claimName, string claimValue)
        {
            _claimName = claimName;
            _claimValue = claimValue;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.HasClaim(_claimName, _claimValue))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
