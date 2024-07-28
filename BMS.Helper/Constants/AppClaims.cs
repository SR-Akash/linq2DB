using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Helper.Constants
{
    public static class AppClaims
    {
        public const string IsRequiredUserCredential = "required_user_credential";
        public const string IsAuthorizationRequired = "authorization_required";

        public const string HasUserId = "has_user_id";
        public const string ClientType = "client_type";
        public const string ClientId = "app_client_id";
        public const string ClientRoleId = "client_role_id";
        public const string ClientName = "client_name";
        public const string ClientTenantId = "client_tenant_id";
        public const string TimeZoneOffset = "time_zone_offset";

        public const string User = "user";
        public const string UserName = "user_name";
        public const string UserPassword = "user_password";
        public const string UserId = "user_id";
        public const string UserEmail = "user_email";
        public const string UserMobile = "user_mobile";
        public const string UserFullName = "user_full_name";
        public const string UserDisplayId = "user_display_id";
        public const string UserRoleId = "user_role_id";
        public const string UserType = "user_role_id";
        public const string AccountVerifiedAt = "account_verified_at";
        public const string AccessToken = "access_token";

    }
}
