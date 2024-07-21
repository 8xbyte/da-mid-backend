using DaMid.Interfaces;
using DaMid.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaMid.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IJwtService jwtService, IAuthService authService) : ControllerBase {
        private readonly IJwtService _jwtService = jwtService;
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult> Login(string login, string password) {
            var user = await _authService.Login(login, password);

            if (user == null) {
                return Unauthorized(new {
                    Message = "Неверное имя пользователя или пароль"
                });
            }

            return Ok(new {
                Token = _jwtService.GenerateToken(new ITokenPayload {
                    UserId = user.Id
                })
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(string login, string password) {
            var user = await _authService.Register(login, password);

            if (user == null) {
                return Unauthorized(new {
                    Message = "Такой пользователь уже существует"
                });
            }

            return Ok(new {
                Token = _jwtService.GenerateToken(new ITokenPayload {
                    UserId = user.Id
                })
            });
        }
    }
}
