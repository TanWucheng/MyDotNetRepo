using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BasicUtilsLibrary.Converters;
using BasicUtilsLibrary.Encryption;
using DaoLibrary;
using DaoLibrary.Entities;
using DaoLibrary.Repositories;
using EntityGeneratorWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EntityGeneratorWebApp.Utils
{
    public class TokenGenerator
    {
        private readonly TokenGenerateOption _option;
        private readonly string _privateKey;

        public IAuthRepository AuthRepository { get; }

        public TokenGenerator(TokenGenerateOption option, IAuthRepository authRepository, string privateKey)
        {
            _option = option;
            AuthRepository = authRepository;
            _privateKey = privateKey;
        }

        async Task BadRequest(HttpContext context, string msg)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(msg);
        }

        internal void GenerateTokenAsync(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
                {
                    await BadRequest(context, "format not support!");
                    return;
                }

                var userIdentity = context.Request.Form["userIdentity"];
                var password = context.Request.Form["password"];
                var platform = context.Request.Form["platform"];
                var databaseType = TryParser.TryParseEnum<DatabaseType>(context.Request.Form["databaseType"]);

                if (platform == "browser")
                {
                    if (Rsa.Decrypt(userIdentity, _privateKey, out var outString1))
                    {
                        userIdentity = outString1;
                    }
                    if (Rsa.Decrypt(password, _privateKey, out var outString2))
                    {
                        password = outString2;
                    }
                }
                else
                {
                    if (Rsa.Decrypt(userIdentity, out var outString1))
                    {
                        userIdentity = outString1;
                    }
                    if (Rsa.Decrypt(password, out var outString2))
                    {
                        password = outString2;
                    }
                }

                IdentityUser identityUser = null;
                if (AuthRepository != null)
                {
                    identityUser = AuthRepository.FindUser(userIdentity, password, databaseType).Result;
                }

                if (identityUser == null)
                {
                    await BadRequest(context, "Invalid user identity or password.");
                    return;
                }

                var now = DateTime.UtcNow;
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.UserIdentity),
                    new Claim(JwtRegisteredClaimNames.UniqueName,identityUser.Password),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
                };

                var jwt = new JwtSecurityToken(_option.Issuer, _option.Audience, claims, now, now.Add(_option.Expiration), _option.SigningCredentials);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)_option.Expiration.TotalSeconds,
                };

                // Serialize and return the response
                context.Response.ContentType = "application/json";
                var responseStr = JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
                await context.Response.WriteAsync(responseStr);
            });
        }
    }
}
