using DaMid.Contexts;
using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Services {
    public interface IUserService {
        public Task<UserModel?> GetUserByIdAsync(int id);
        public Task<UserModel?> GetUserByLoginAsync(string login);
        public Task<UserModel> CreateUserAsync(UserModel user);
        public Task<UserModel> UpdateUserAsync(UserModel user);
        public Task<UserModel> RemoveUserAsync(UserModel user);
    }

    public class UserService(ApplicationContext context) : IUserService {
        private readonly ApplicationContext _context = context;

        public async Task<UserModel?> GetUserByIdAsync(int id) {
            return await _context.Users.FirstOrDefaultAsync(model => model.Id == id);
        }
        public async Task<UserModel?> GetUserByLoginAsync(string login) {
            return await _context.Users.FirstOrDefaultAsync(model => model.Login == login);
        }
        public async Task<UserModel> CreateUserAsync(UserModel userModel) {
            var user = await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return user.Entity;
        }
        public async Task<UserModel> UpdateUserAsync(UserModel userModel) {
            var user = _context.Users.Update(userModel);
            await _context.SaveChangesAsync();
            return user.Entity;
        }
        public async Task<UserModel> RemoveUserAsync(UserModel userModel) {
            var user = _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return user.Entity;
        }
    }
}
