using DaMid.Contexts;
using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Services {
    public interface IGroupService {
        public Task<GroupModel?> GetGroupByIdAsync(int id);
        public Task<GroupModel?> GetGroupByNameAsync(string name);
        public Task<List<GroupModel>> GetGroupsAsync(int offset, int limit);
        public Task<List<GroupModel>> SearchGroupsAsync(string name, int offset, int limit);
        public Task<GroupModel> AddGroupAsync(GroupModel classModel);
        public Task<GroupModel> RemoveGroupAsync(GroupModel classModel);
    }

    public class GroupService(ApplicationContext context) : IGroupService {
        private readonly ApplicationContext _context = context;

        public async Task<GroupModel?> GetGroupByIdAsync(int id) {
            return await _context.Groups.FirstOrDefaultAsync(model => model.Id == id);
        }

        public async Task<GroupModel?> GetGroupByNameAsync(string name) {
            return await _context.Groups.FirstOrDefaultAsync(model => model.Name == name);
        }

        public async Task<List<GroupModel>> GetGroupsAsync(int offset, int limit) {
            return await _context.Groups.OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<List<GroupModel>> SearchGroupsAsync(string name, int offset, int limit) {
            return await _context.Groups.Where(model => EF.Functions.Like(model.Name.ToLower(), $"%{name.ToLower()}%")).OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<GroupModel> AddGroupAsync(GroupModel groupModel) {
            var group = await _context.Groups.AddAsync(groupModel);
            await _context.SaveChangesAsync();
            return group.Entity;
        }

        public async Task<GroupModel> RemoveGroupAsync(GroupModel groupModel) {
            var group = _context.Groups.Remove(groupModel);
            await _context.SaveChangesAsync();
            return group.Entity;
        }
    }
}