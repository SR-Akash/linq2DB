using BMS.Core.Interfaces.Security;
using BMS.DTO.Common;
using BMS.DTO.Common.Security;
using BMS.DTO.Response;
using BMS.Infrastructure;
using DataModels;
using LinqToDB;
using LinqToDB.SqlQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using static LinqToDB.Common.Configuration;

namespace BMS.Core.Service.Security
{
    public class UserService : IUserService
    {
        private IOptions<Audience> _settings;


        public UserService(IOptions<Audience> settings)
        {
            this._settings = settings;
        }

        public async Task<AuthDTO> LoginWithGmail(string token)
        {
            try
            {
                using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                CancellationToken cancellationToken = default;
                if (!string.IsNullOrEmpty(token))
                {
                    using var _context = new AppDB();
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwt = tokenHandler.ReadJwtToken(token);

                    string gmail = jwt is not null ? jwt.Claims.FirstOrDefault(x => x.Type == "email").Value : String.Empty;
                    bool email_verified = jwt is not null ? Convert.ToBoolean(jwt.Claims.FirstOrDefault(x => x.Type == "email_verified").Value) : false;
                    string name = jwt is not null ? jwt.Claims.FirstOrDefault(x => x.Type == "name").Value : String.Empty;
                    string nbf = jwt is not null ? jwt.Claims.FirstOrDefault(x => x.Type == "nbf").Value : String.Empty;
                    gmail = gmail.ToLower();
                    if (email_verified == false)
                    {
                        throw new Exception("Email verification is failed.");
                    }
                    var jwtToken = await GenerateToken(gmail);

                    if (!string.IsNullOrEmpty(gmail))
                    {
                        var userExist = await _context.TblUsers.Where(x => x.StrLoginId == gmail).Select(x => x).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                        var flatExist = await _context.TblFlatInformation.Where(x => x.Email == gmail).Select(x => x).ToListAsync(cancellationToken).ConfigureAwait(false);

                        if (userExist == null)
                        {
                            var user = new TblUser();
                            if (flatExist.Count() == 0)
                            {
                                user = new TblUser
                                {
                                    DteLastActionDateTime = DateTime.Now,
                                    DtePasswordExpDate = DateTime.Now.AddDays(180),
                                    IsSuperUser = true,
                                    IntActionBy = 0,
                                    IntUserType = 1,
                                    IsActive = true,
                                    StrEmailAddress = gmail,
                                    StrContact = nbf,
                                    StrUserName = name,
                                    StrLoginId = gmail

                                };

                            }
                            else
                            {
                                {
                                    user = new TblUser
                                    {

                                        DteLastActionDateTime = DateTime.Now,
                                        DtePasswordExpDate = DateTime.Now.AddDays(180),
                                        IsSuperUser = false,
                                        IntActionBy = 0,
                                        IntUserType = 1,
                                        IsActive = true,
                                        StrEmailAddress = gmail,
                                        StrContact = nbf,
                                        StrUserName = name,
                                        StrLoginId = gmail

                                    };
                                }
                            }

                            var userId = await _context.InsertWithInt32IdentityAsync(user).ConfigureAwait(false);
                            userExist = await _context.TblUsers.Where(x => x.IntUserId == userId).Select(x => x).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                        }

                        var distinctBuildingId = flatExist.Select(x => x.BuildingId).Distinct();
                        var existingBuildingInfo = await _context.TblUserBuildingInfo.Where(x => distinctBuildingId.Contains(x.BuildingId)).Select(x => x.BuildingId).Distinct().ToListAsync(cancellationToken).ConfigureAwait(false);
                        var uniqueBuildingId = distinctBuildingId.Except(existingBuildingInfo).ToList();

                        if (uniqueBuildingId.Count() > 0)
                        {
                            var buildingData = uniqueBuildingId.Select(x => new TblUserBuildingInfo
                            {
                                IntUserId = userExist.IntUserId,
                                BuildingId = x,
                                DteServerDateTime = DateTime.Now,
                                IntActionBy = userExist.IntUserId,
                                IsActive = true
                            }).ToList();

                            await _context.InsertWithInt32IdentityAsync(buildingData).ConfigureAwait(false);
                        }


                        return new AuthDTO
                        {
                            Token = jwtToken.Token,
                            RefreshToken = jwtToken.Token,
                            ActionTime = jwtToken.ActionTime,
                            expires_in = jwtToken.expires_in,
                            Success = jwtToken.Success,
                            Contact = userExist.StrContact,
                            Email = userExist.StrEmailAddress,
                            UserId = userExist.IntUserId,
                            UserName = userExist.StrUserName
                        };
                    }
                    else
                    {
                        throw new Exception("Invalid Email");
                    }
                }
                else
                {
                    throw new Exception("Invalid Data");
                }
                transaction.Complete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AuthDTO> GenerateToken(string gmail)
        {
            var now = DateTime.UtcNow;
            try
            {
                var claims = new Claim[]
                {

                    new Claim(ClaimTypes.Role,"test"),
                    //new Claim("enroll",AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916","test")),
                    //new Claim("terantId",AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", "Test")),
                    new Claim("gmail",gmail),
                    //new Claim("accountId",accountId.ToString()),
                    //new Claim("branchId",branchId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, "test"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                };

                //_env.IsProduction()? Configuration.GetSection("REACT_APP_SECRET_VALUE").Value.Trim():

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));

                var jwt = new JwtSecurityToken(
                    issuer: _settings.Value.Iss,
                    audience: _settings.Value.Aud,
                    claims: claims,
                    //notBefore: now,
                    // expires: now.Add(TimeSpan.FromMinutes(60 * 24 * 1)), //expires after 1day
                    expires: now.Add(TimeSpan.FromHours(60 * 24 * 1)), //expires after 1day
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


                return new AuthDTO
                {
                    Success = true,
                    Token = encodedJwt,
                    RefreshToken = encodedJwt,
                    expires_in = (int)TimeSpan.FromMinutes(1).TotalSeconds,
                    ActionTime = DateTime.UtcNow
                };

            }
            catch (Exception ex)
            {
                throw new Exception("data not match");
            }
        }
    }
}
