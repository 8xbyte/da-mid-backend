using DaMid.Interfaces;
using DaMid.Interfaces.Data;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaMid.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IJwtService jwtService, IAuthService authService) : ControllerBase {
        private readonly IJwtService _jwtService = jwtService;
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] ILoginData loginData) {
            var user = await _authService.LoginAsync(loginData.Login, loginData.Password);

            if (user == null) {
                return BadRequest(new {
                    Message = "Неверное имя пользователя или пароль"
                });
            }

            return Ok(new {
                Token = _jwtService.GenerateToken(new ITokenPayload {
                    UserId = user.Id,
                    Role = user.Role
                })
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] IRegisterData registerData) {
            var user = await _authService.RegisterAsync(registerData.Login, registerData.Password);

            if (user == null) {
                return BadRequest(new {
                    Message = "Такой пользователь уже существует"
                });
            }

            return Ok(new {
                Token = _jwtService.GenerateToken(new ITokenPayload {
                    UserId = user.Id,
                    Role = user.Role
                })
            });
        }
    }
}
