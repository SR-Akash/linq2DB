using CryptoHelper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using BMS.Core.Helpers;
using BMS.Core.Interfaces.Security;
using BMS.DTO.Response;

using BMS.Helper.Constants;
using BMS.Helper.Enums;
using BMS.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Microsoft.AspNetCore.Authorization;

namespace BMS.API.Controllers.Security
{
    [Route("bms/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorizationController : ControllerBase
    {

        //private readonly OpenIddictApplicationManager<ApplicationClient> _applicationClientManager;
        private readonly IUserService _iUserService;
        private IStringLocalizer<SharedResource> _localizer;
        private readonly ICurrentSessionHelper _currentSessionHelper;


        public AuthorizationController(

            IUserService iUserService
            , IStringLocalizer<SharedResource> localizer
           , ICurrentSessionHelper currentSessionHelper
           )
        {

            //_applicationClientManager = applicationClientManager;
            _iUserService = iUserService;
            _localizer = localizer;
            _currentSessionHelper = currentSessionHelper;
        }

        [HttpPost]
        [Route("LoginWithGmail")]
        public async Task<IActionResult> LoginWithGmail(string token)
        {
            try
            {
                var msg = await _iUserService.LoginWithGmail(token);
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }


        //[HttpPost("token")]
        //public async Task<IActionResult> Exchange()
        //{
        //    var request = HttpContext.GetOpenIddictServerRequest() ??
        //        throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        //    ClaimsPrincipal claimsPrincipal;

        //    try
        //    {
        //        var uName = request.GetParameter("username")?.Value;
        //        var uPass = request.GetParameter("password")?.Value;

        //        var offset = request.GetParameter("offset")?.Value;
        //        if (string.IsNullOrEmpty(Convert.ToString(offset)))
        //        {
        //            offset = "0";
        //        }

        //        if (string.IsNullOrEmpty(request.Username))
        //        {
        //            request.Username = Convert.ToString(uName);
        //        }
        //        if (string.IsNullOrEmpty(request.Password))
        //        {
        //            request.Password = Convert.ToString(uPass);
        //        }

        //        var client = await _applicationClientManager.FindByClientIdAsync(request.ClientId);

        //        //  Note: This is not required, OpenIddict automatically validate client credential.
        //        if (!Crypto.VerifyHashedPassword(client.ClientSecret, request.ClientSecret))
        //        {
        //            return new ContentResult() { Content = _localizer[StringHelper.InvalidClientCredential], StatusCode = 401 };
        //        }
        //        //  Check for in-active client
        //        if (client.ClientStatus != ClientStatus.Active.ToString())
        //        {
        //            return new ContentResult() { Content = _localizer[StringHelper.InvalidClientStatus], StatusCode = 401 };
        //        }

        //        if (request.IsClientCredentialsGrantType())
        //        {
        //            #region Client Validation

        //            // Client !IsRequiredUserCredential and  !IsAuthorizationRequired : Client can access any resources without user credential
        //            // Client !IsRequiredUserCredential and   IsAuthorizationRequired : Client can only access authorize resources without user credential
        //            // Client  IsRequiredUserCredential and   IsAuthorizationRequired : Client can only access authorize resources with user credential
        //            // Client  IsRequiredUserCredential and  !IsAuthorizationRequired : Client can't access any resources

        //            if (client.IsRequiredUserCredential && !client.IsAuthorizationRequired)
        //            {
        //                return new ContentResult() { Content = _localizer[StringHelper.InvalidClientConfiguration], StatusCode = 401 };
        //            }

        //            if (client.IsRequiredUserCredential && client.IsAuthorizationRequired && (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password)))
        //            {
        //                return new ContentResult() { Content = _localizer[StringHelper.UserInformationRequired], StatusCode = 401 };
        //            }


        //            #endregion

        //            #region User Validation
        //            User user = null;

        //            if (!string.IsNullOrEmpty(request.Username))
        //            {
        //                user = await _iUserService.GetAsync(request.Username);

        //                if (user == null)
        //                {
        //                    return new ContentResult() { Content = _localizer[StringHelper.UserInformationNotFound], StatusCode = 400 };
        //                }

        //                // Check User Status
        //                if (user.Status != GeneralStatus.Active.ToString())
        //                {
        //                    return new ContentResult() { Content = _localizer[StringHelper.UserStatusNotValid], StatusCode = 400 };
        //                }

        //                //Check User Password
        //                if (!Crypto.VerifyHashedPassword(user.Password, request.Password))
        //                {
        //                    return new ContentResult() { Content = _localizer[StringHelper.UserCredentialNotValid], StatusCode = 400 };
        //                }
        //            }

        //            #endregion

        //            #region Create Claim

        //            // Create a new ClaimsIdentity holding the user identity.
        //            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);


        //            // Add a "sub" claim containing the user identifier, and attach
        //            // the "access_token" destination to allow OpenIddict to store it
        //            // in the access token, so it can be retrieved from your controllers.

        //            var userId = 0;
        //            var userFullname = "";
        //            var userEmail = "";
        //            var userMobile = "";
        //            var userRoleId = 0;
        //            var userType = "";


        //            identity.AddClaim(Claims.Subject, Convert.ToString(Guid.NewGuid()), Destinations.AccessToken);
        //            identity.AddClaim(Claims.ClientId, Convert.ToString(client.ClientId), Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.ClientId, Convert.ToString(client.Id), Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.ClientRoleId, Convert.ToString(client.ClientRoleId), Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.ClientName, Convert.ToString(client.DisplayName), Destinations.AccessToken);

        //            identity.AddClaim(AppClaims.IsRequiredUserCredential, client.IsRequiredUserCredential == true ? "Yes" : "No", Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.IsAuthorizationRequired, client.IsAuthorizationRequired == true ? "Yes" : "No", Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.ClientTenantId, Convert.ToString(client.ClientTenantId), Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.TimeZoneOffset, Convert.ToString(offset), Destinations.AccessToken);

        //            if (user != null)
        //            {
        //                // We will reduce this call
        //                user = await _iUserService.GetAsync(request.Username);

        //                userId = user.Id;
        //                userFullname = user.FullName;
        //                userRoleId = user.RoleId;
        //                userEmail = user.Email;
        //                userMobile = user.Mobile;
        //                userType = user.UserType;

        //                identity.AddClaim(AppClaims.UserName, user.UserName, Destinations.AccessToken);
        //                identity.AddClaim(AppClaims.UserId, user.Id.ToString(), Destinations.AccessToken);
        //                identity.AddClaim(AppClaims.UserRoleId, user.RoleId.ToString(), Destinations.AccessToken);
        //                identity.AddClaim(AppClaims.UserFullName, user.FullName, Destinations.AccessToken);

        //            }

        //            #endregion

        //            #region Generate Ticket

        //            claimsPrincipal = new ClaimsPrincipal(identity);

        //            // You have to grant the 'offline_access' scope to allow
        //            // OpenIddict to return a refresh token to the caller.
        //            // claimsPrincipal.SetScopes(Scopes.OfflineAccess);

        //            var auth = new Dictionary<string, object>
        //            {
        //                ["userId"] = userId,
        //                ["clientId"] = client.Id,
        //                ["clientName"] = client.DisplayName,
        //                ["userName"] = userFullname,
        //                ["userMobile"] = userMobile ?? "",
        //                ["UserEmail"] = userEmail,
        //                ["userRoleId"] = userRoleId,
        //                ["userType"] = userType
        //            };

        //            string authObj = userId > 0 ? JsonConvert.SerializeObject(auth) : "";

        //            var properties = new AuthenticationProperties(
        //                items: new Dictionary<string, string>(),
        //                parameters:
        //                new Dictionary<string, object>
        //                {
        //                    ["user"] = authObj,
        //                }
        //                );

        //            return SignIn(claimsPrincipal, properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        //            #endregion

        //        }
        //        else if (request.IsRefreshTokenGrantType())
        //        {
        //            //// There is a security concern while generating access token with refresh token, because we can't validate subscriber information

        //            //// Retrieve the claims principal stored in the refresh token.

        //            var info = await HttpContext.AuthenticateAsync(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        //            var userName = info.Principal.Claims.FirstOrDefault(c => c.Type == AppClaims.UserName)?.Value;
        //            var UserId = info.Principal.Claims.FirstOrDefault(c => c.Type == AppClaims.UserId)?.Value;
        //            var roleId = info.Principal.Claims.FirstOrDefault(c => c.Type == AppClaims.UserRoleId)?.Value;
        //            var name = info.Principal.Claims.FirstOrDefault(c => c.Type == AppClaims.UserFullName)?.Value;

        //            //// Create a new ClaimsIdentity holding the user identity.
        //            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        //            //// Add a "sub" claim containing the user identifier, and attach
        //            //// the "access_token" destination to allow OpenIddict to store it
        //            //// in the access token, so it can be retrieved from your controllers.

        //            identity.AddClaim(Claims.Subject, Convert.ToString(Guid.NewGuid()), Destinations.AccessToken);
        //            identity.AddClaim(Claims.ClientId, Convert.ToString(client.ClientId), Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.ClientId, Convert.ToString(client.Id), Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.ClientRoleId, Convert.ToString(client.ClientRoleId), Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.IsRequiredUserCredential, client.IsRequiredUserCredential == true ? "Yes" : "No", Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.IsAuthorizationRequired, client.IsAuthorizationRequired == true ? "Yes" : "No", Destinations.AccessToken);
        //            identity.AddClaim(AppClaims.ClientTenantId, Convert.ToString(client.ClientTenantId), Destinations.AccessToken);

        //            if (userName != null)
        //            {
        //                identity.AddClaim(AppClaims.UserName, userName, Destinations.AccessToken);
        //                identity.AddClaim(AppClaims.UserId, UserId, Destinations.AccessToken);
        //                identity.AddClaim(AppClaims.UserRoleId, roleId, Destinations.AccessToken);
        //                identity.AddClaim(AppClaims.UserFullName, name, Destinations.AccessToken);
        //            }


        //            #region Generate Ticket

        //            claimsPrincipal = new ClaimsPrincipal(identity);

        //            // You have to grant the 'offline_access' scope to allow
        //            // OpenIddict to return a refresh token to the caller.
        //            //claimsPrincipal.SetScopes(Scopes.OfflineAccess);

        //            var properties = new AuthenticationProperties(
        //                items: new Dictionary<string, string>(),
        //                parameters: new Dictionary<string, object>
        //                {
        //                    ["userId"] = UserId,
        //                    ["clientId"] = Convert.ToString(client.Id),
        //                    ["clientName"] = client.DisplayName,
        //                    ["userName"] = name,

        //                });

        //            return SignIn(claimsPrincipal, properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        //            #endregion
        //        }

        //        return new ObjectResult(SetResponses.SetErrorResponse("Invalid Request"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
        //    }



        //}

        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    var info = await HttpContext.AuthenticateAsync(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        //    var result = SignOut(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        //    return new ObjectResult(SetResponses.SetSuccessResponse());
        //}


    }
}
