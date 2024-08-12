using DaMid.Contexts;
using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Services {
    public interface ITeacherService {
        public Task<TeacherModel?> GetTeacherByIdAsync(int id);
        public Task<TeacherModel?> GetTeacherByNameAsync(string name);
        public Task<TeacherModel?> GetTeacherBySurnameAsync(string surname);
        public Task<TeacherModel?> GetTeacherByNameAndSurnameAsync(string name, string surname);
        public Task<List<TeacherModel>> GetTeachersAsync(int offset, int limit);
        public Task<List<TeacherModel>> SearchTeachersAsync(string name, string surname, int offset, int limit);
        public Task<TeacherModel> AddTeacherAsync(TeacherModel teacherModel);
        public Task<TeacherModel> RemoveTeacherAsync(TeacherModel teacherModel);
    }

    public class TeacherService(ApplicationContext context) : ITeacherService {
        private readonly ApplicationContext _context = context;

        public async Task<TeacherModel?> GetTeacherByIdAsync(int id) {
            return await _context.Teachers.FirstOrDefaultAsync(model => model.Id == id);
        }

        public async Task<TeacherModel?> GetTeacherByNameAsync(string name) {
            return await _context.Teachers.FirstOrDefaultAsync(model => model.Name == name);
        }

        public async Task<TeacherModel?> GetTeacherBySurnameAsync(string surname) {
            return await _context.Teachers.FirstOrDefaultAsync(model => model.Surname == surname);
        }

        public async Task<TeacherModel?> GetTeacherByNameAndSurnameAsync(string name, string surname) {
            return await _context.Teachers.FirstOrDefaultAsync(model => model.Name == name && model.Surname == surname);
        }

        public async Task<List<TeacherModel>> GetTeachersAsync(int offset, int limit) {
            return await _context.Teachers.OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<List<TeacherModel>> SearchTeachersAsync(string name, string surname, int offset, int limit) {
            return await _context.Teachers.Where(model => EF.Functions.Like(model.Name.ToLower(), $"%{name.ToLower()}%") || EF.Functions.Like(model.Surname.ToLower(), $"%{surname.ToLower()}%")).OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<TeacherModel> AddTeacherAsync(TeacherModel teacherModel) {
            var teacher = await _context.Teachers.AddAsync(teacherModel);
            await _context.SaveChangesAsync();
            return teacher.Entity;
        }

        public async Task<TeacherModel> RemoveTeacherAsync(TeacherModel teacherModel) {
            var teacher = _context.Teachers.Remove(teacherModel);
            await _context.SaveChangesAsync();
            return teacher.Entity;
        }
    }
}