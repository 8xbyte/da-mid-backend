using DaMid.Contexts;
using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Services {
    public interface IClassService {
        public Task<ClassModel?> GetClassByIdAsync(int id);
        public Task<ClassModel?> GetClassByNameAsync(string name);
        public Task<List<ClassModel>> GetClassesAsync(int offset, int limit);
        public Task<List<ClassModel>> SearchClassesAsync(string name, int limit);
        public Task<ClassModel> AddClassAsync(ClassModel classModel);
        public Task<ClassModel> RemoveClassAsync(ClassModel classModel);
    }

    public class ClassService(ApplicationContext context) : IClassService {
        private readonly ApplicationContext _context = context;

        public async Task<ClassModel?> GetClassByIdAsync(int id) {
            return await _context.Classes.FirstOrDefaultAsync(model => model.Id == id);
        }

        public async Task<ClassModel?> GetClassByNameAsync(string name) {
            return await _context.Classes.FirstOrDefaultAsync(model => model.Name == name);
        }

        public async Task<List<ClassModel>> GetClassesAsync(int offset, int limit) {
            return await _context.Classes.OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<List<ClassModel>> SearchClassesAsync(string name, int limit) {
            return await _context.Classes.Where(model => EF.Functions.Like(model.Name.ToLower(), $"%{name.ToLower()}%")).Take(limit).ToListAsync();
        }

        public async Task<ClassModel> AddClassAsync(ClassModel classModel) {
            var _class = await _context.Classes.AddAsync(classModel);
            await _context.SaveChangesAsync();
            return _class.Entity;
        }

        public async Task<ClassModel> RemoveClassAsync(ClassModel classModel) {
            var _class = _context.Classes.Remove(classModel);
            await _context.SaveChangesAsync();
            return _class.Entity;
        }
    }
}