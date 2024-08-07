using DaMid.Contexts;
using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Services {
    public interface IUserService {
        public Task<UserModel?> GetUserById(int id);
        public Task<UserModel?> GetUserByLogin(string login);
        public Task<UserModel> CreateUser(UserModel user);
        public Task<UserModel> UpdateUser(UserModel user);
        public Task<UserModel> RemoveUser(UserModel user);
    }

    public class UserService(ApplicationContext context) : IUserService {
        private readonly ApplicationContext _context = context;

        public async Task<UserModel?> GetUserById(int id) {
            return await _context.Users.FirstOrDefaultAsync(model => model.Id == id);
        }
        public async Task<UserModel?> GetUserByLogin(string login) {
            return await _context.Users.FirstOrDefaultAsync(model => model.Login == login);
        }
        public async Task<UserModel> CreateUser(UserModel userModel) {
            var user = await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return user.Entity;
        }
        public async Task<UserModel> UpdateUser(UserModel userModel) {
            var user = _context.Users.Update(userModel);
            await _context.SaveChangesAsync();
            return user.Entity;
        }
        public async Task<UserModel> RemoveUser(UserModel userModel) {
            var user = _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return user.Entity;
        }
    }
}
