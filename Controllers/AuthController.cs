using DaMid.Interfaces.Data;
using DaMid.Interfaces.Options;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DaMid.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService, IOptions<ICookieOptions> cookieOptions) : ControllerBase {
        private readonly IAuthService _authService = authService;
        private readonly ICookieOptions _cookieOptions = cookieOptions.Value;

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] ILoginData loginData) {
            var token = await _authService.LoginAsync(loginData.Login, loginData.Password);
            if (token == null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Неверное имя пользователя или пароль"
                });
            }

            HttpContext.Response.Cookies.Append(_cookieOptions.JwtToken, token, new CookieOptions {
                Expires = DateTime.Now.AddSeconds(_cookieOptions.Expiration),
                Domain = _cookieOptions.Domain,
                HttpOnly = true
            });

            return Ok(new {
                Status = "ok"
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] IRegisterData registerData) {
            var token = await _authService.RegisterAsync(registerData.Login, registerData.Password);
            if (token == null) {
                return BadRequest(new {
                    Status = "error",
                    Message = "Такой пользователь уже существует"
                });
            }

            HttpContext.Response.Cookies.Append(_cookieOptions.JwtToken, token, new CookieOptions {
                Expires = DateTime.Now.AddSeconds(_cookieOptions.Expiration),
                Domain = _cookieOptions.Domain,
                HttpOnly = true
            });

            return Ok(new {
                Status = "ok"
            });
        }
    }
}
