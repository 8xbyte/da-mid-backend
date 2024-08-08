using DaMid.Contexts;
using DaMid.Models;
using DaMid.Interfaces;

namespace DaMid.Services {
    public interface IAuthService {
        public Task<string?> LoginAsync(string email, string password);
        public Task<string?> RegisterAsync(string email, string password);
    }

    public class AuthService(IUserService userService, IJwtService jwtService) : IAuthService {
        private readonly IUserService _userService = userService;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<string?> LoginAsync(string login, string password) {
            var user = await _userService.GetUserByLoginAsync(login);
            if (user == null) {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) {
                return null;
            }

            return _jwtService.GenerateToken(new ITokenPayload {
                UserId = user.Id,
                Role = user.Role
            });
        }
        public async Task<string?> RegisterAsync(string login, string password) {
            var user = await _userService.GetUserByLoginAsync(login);
            if (user != null) {
                return null;
            }

            var newUser = await _userService.CreateUserAsync(new UserModel {
                Login = login,
                Role = UserRole.User,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                RegistrationDate = DateTime.UtcNow
            });

            return _jwtService.GenerateToken(new ITokenPayload {
                UserId = newUser.Id,
                Role = newUser.Role
            });
        }
    }
}
