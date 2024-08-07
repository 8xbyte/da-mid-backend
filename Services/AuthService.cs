using DaMid.Contexts;
using DaMid.Models;

namespace DaMid.Services {
    public interface IAuthService {
        public Task<UserModel?> Login(string email, string password);
        public Task<UserModel?> Register(string email, string password);
    }

    public class AuthService(IUserService userService) : IAuthService {
        private readonly IUserService _userService = userService;

        public async Task<UserModel?> Login(string login, string password) {
            var user = await _userService.GetUserByLogin(login);
            if (user == null) {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.Password)) {
                return user;
            }

            return null;
        }
        public async Task<UserModel?> Register(string login, string password) {
            var user = await _userService.GetUserByLogin(login);
            if (user != null) {
                return null;
            }

            var createdUser = await _userService.CreateUser(new UserModel {
                Login = login,
                Role = UserRole.User,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                RegistrationDate = DateTime.UtcNow
            });
            return createdUser;
        }
    }
}
