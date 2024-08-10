using DaMid.Interfaces.Options;
using DaMid.Models;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DaMid.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizationAttribute(UserRole role) : Attribute, IAuthorizationFilter {
        public void OnAuthorization(AuthorizationFilterContext context) {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtService>();
            var cookieOptions = context.HttpContext.RequestServices.GetService<IOptions<ICookieOptions>>()?.Value;

            var token = context.HttpContext.Request.Cookies[cookieOptions!.JwtToken];
            if (token.IsNullOrEmpty()) {
                context.Result = new UnauthorizedObjectResult(new {
                    Status = "error",
                    Result = "not found token"
                }); return;
            }

            var tokenPayload = jwtService!.VerifyToken(token!);
            if (tokenPayload == null) {
                context.Result = new UnauthorizedObjectResult(new {
                    Status = "error",
                    Result = "bad token"
                }); return;
            }

            if (tokenPayload!.Role < role) {
                context.Result = new ObjectResult(new {
                    Status = "error",
                    Result = "access denied"
                }) {
                    StatusCode = StatusCodes.Status403Forbidden
                }; return;
            }

            context.HttpContext.Items.Add(cookieOptions.JwtToken, tokenPayload);
        }
    }
}