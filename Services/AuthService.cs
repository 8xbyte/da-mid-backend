using DaMid.Contexts;
using DaMid.Models;

namespace DaMid.Services {
    public interface IAuthService {
        public Task<UserModel?> LoginAsync(string email, string password);
        public Task<UserModel?> RegisterAsync(string email, string password);
    }

    public class AuthService(IUserService userService) : IAuthService {
        private readonly IUserService _userService = userService;

        public async Task<UserModel?> LoginAsync(string login, string password) {
            var user = await _userService.GetUserByLoginAsync(login);
            if (user == null) {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.Password)) {
                return user;
            }

            return null;
        }
        public async Task<UserModel?> RegisterAsync(string login, string password) {
            var user = await _userService.GetUserByLoginAsync(login);
            if (user != null) {
                return null;
            }

            return await _userService.CreateUserAsync(new UserModel {
                Login = login,
                Role = UserRole.User,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                RegistrationDate = DateTime.UtcNow
            });
        }
    }
}
