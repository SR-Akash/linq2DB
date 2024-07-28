using Microsoft.AspNetCore.Http;
using BMS.DTO.Common;
using BMS.Helper.Constants;
using System;
using System.Linq;

namespace BMS.Core.Helpers
{
    /// <summary>
    /// Session Helper
    /// </summary>
    public class CurrentSessionHelper : ICurrentSessionHelper
    {
        private readonly IHttpContextAccessor _accessor;
        public CurrentSessionHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Get Current Session
        /// </summary>
        /// <returns></returns>
        public CurrentSession GetCurrentSession()
        {
            try
            {
                var context = _accessor.HttpContext;

                if (context != null && context.User.Identity.IsAuthenticated)
                {
                    var clientId = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.ClientId)?.Value;
                    var isRequiredUserCredential = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.IsRequiredUserCredential)?.Value;
                    var isAuthorizationRequired = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.IsAuthorizationRequired)?.Value;
                    var clientTenantId = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.ClientTenantId)?.Value;

                    var Username = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserName)?.Value;
                    var UserId = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserId)?.Value;
                    var RoleId = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserRoleId)?.Value;
                    var Name = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserFullName)?.Value;
                    var offset = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.TimeZoneOffset)?.Value;
                    var displayId = context.User.Claims.FirstOrDefault(c => c.Type == AppClaims.UserDisplayId)?.Value;


                    var session = new CurrentSession();
                    session.ClientId = Convert.ToInt32(clientId);
                    session.IsRequiredUserCredential = isRequiredUserCredential;
                    session.IsAuthorizationRequired = isAuthorizationRequired;
                    session.TenantId = Convert.ToInt32(clientTenantId);

                    session.UserId = !string.IsNullOrEmpty(UserId) ? Convert.ToInt32(UserId) : null;
                    session.UserFullName = Name;
                    session.UserRoleId = !string.IsNullOrEmpty(RoleId) ? Convert.ToInt32(RoleId) : 0; ;
                    session.UserFullName = Name;
                    session.DisplayId = displayId;

                    int timeZoneOffset;
                    bool isParsable = Int32.TryParse(offset, out timeZoneOffset);

                    session.TimeZoneOffset = Convert.ToInt32(timeZoneOffset);

                    return session;

                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
    /// <summary>
    /// Sesson Manager
    /// </summary>
    public interface ICurrentSessionHelper
    {
        /// <summary>
        /// Get Current Session
        /// </summary>
        /// <returns></returns>
        CurrentSession GetCurrentSession();
    }
}
