using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using BMS.Helper.Constants;
using BMS.Localization;
using System.Linq;
using System.Security.Claims;

namespace BMS.API
{
    public class CheckPermissionAttribute : TypeFilterAttribute
    {

        public CheckPermissionAttribute() : base(typeof(CheckPermissionFilter))
        {

        }
    }

    public class CheckPermissionFilter : IAuthorizationFilter
    {
        private IStringLocalizer<SharedResource> _localizer;
        private readonly Claim _claim;
        public CheckPermissionFilter(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var clientId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppClaims.ClientId)?.Value;
                var isRequiredUserCredential = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppClaims.IsRequiredUserCredential)?.Value;
                var isAuthorizationRequired = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppClaims.IsAuthorizationRequired)?.Value;
                var clientTenantId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppClaims.ClientTenantId)?.Value;

                var Username = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserName)?.Value;
                var UserId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserId)?.Value;
                var RoleId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserRoleId)?.Value;
                var Name = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserFullName)?.Value;

           
                var path = context.HttpContext.Request.Path;

                if (isRequiredUserCredential.Equals("Yes") && string.IsNullOrEmpty(Username))
                {
                    context.Result = new ContentResult() { Content = _localizer[StringHelper.UnAuthorizeAccess], StatusCode = 401 };
                }

                // ToDo: We will implement client permission here.

            }
            else
            {
                context.Result = new ContentResult() { Content = _localizer[StringHelper.UnAuthorizeAccess], StatusCode = 401 };
            }

        }
    }
}
