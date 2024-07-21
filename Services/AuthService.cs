using DaMid.Contexts;
using DaMid.Models;

namespace DaMid.Services {
    public interface IAuthService {
        public Task<UserModel?> Login(string email, string password);
        public Task<UserModel?> Register(string email, string password);
    }

    public class AuthService(IUserService userService) : IAuthService {
        private readonly IUserService _userService = userService;

        public async Task<UserModel?> Login(string email, string password) {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.Password)) {
                return user;
            }

            return null;
        }
        public async Task<UserModel?> Register(string email, string password) {
            var user = await _userService.GetUserByEmail(email);
            if (user != null) {
                return null;
            }

            var createdUser = await _userService.CreateUser(new UserModel {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            });
            return createdUser;
        }
    }
}
