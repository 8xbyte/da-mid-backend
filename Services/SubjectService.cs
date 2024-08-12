using DaMid.Contexts;
using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Services {
    public interface ISubjectService {
        public Task<SubjectModel?> GetSubjectByIdAsync(int id);
        public Task<SubjectModel?> GetSubjectByNameAsync(string name);
        public Task<List<SubjectModel>> GetSubjectsAsync(int offset, int limit);
        public Task<List<SubjectModel>> SearchSubjectsAsync(string name, int offset, int limit);
        public Task<SubjectModel> AddSubjectAsync(SubjectModel classModel);
        public Task<SubjectModel> RemoveSubjectAsync(SubjectModel classModel);
    }

    public class SubjectService(ApplicationContext context) : ISubjectService {
        private readonly ApplicationContext _context = context;

        public async Task<SubjectModel?> GetSubjectByIdAsync(int id) {
            return await _context.Subjects.FirstOrDefaultAsync(model => model.Id == id);
        }

        public async Task<SubjectModel?> GetSubjectByNameAsync(string name) {
            return await _context.Subjects.FirstOrDefaultAsync(model => model.Name == name);
        }

        public async Task<List<SubjectModel>> GetSubjectsAsync(int offset, int limit) {
            return await _context.Subjects.OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<List<SubjectModel>> SearchSubjectsAsync(string name, int offset, int limit) {
            return await _context.Subjects.Where(model => EF.Functions.Like(model.Name.ToLower(), $"%{name.ToLower()}%")).OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<SubjectModel> AddSubjectAsync(SubjectModel subjectModel) {
            var subject = await _context.Subjects.AddAsync(subjectModel);
            await _context.SaveChangesAsync();
            return subject.Entity;
        }

        public async Task<SubjectModel> RemoveSubjectAsync(SubjectModel subjectModel) {
            var subject = _context.Subjects.Remove(subjectModel);
            await _context.SaveChangesAsync();
            return subject.Entity;
        }
    }
}