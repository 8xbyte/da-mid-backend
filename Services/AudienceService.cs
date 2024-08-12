using DaMid.Contexts;
using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Services {
    public interface IAudienceService {
        public Task<AudienceModel?> GetAudienceByIdAsync(int id);
        public Task<AudienceModel?> GetAudienceByNameAsync(string name);
        public Task<List<AudienceModel>> GetAudiencesAsync(int offset, int limit);
        public Task<List<AudienceModel>> SearchAudiencesAsync(string name, int offset, int limit);
        public Task<AudienceModel> AddAudienceAsync(AudienceModel audienceModel);
        public Task<AudienceModel> RemoveAudienceAsync(AudienceModel audienceModel);
    }

    public class AudienceService(ApplicationContext context) : IAudienceService {
        private readonly ApplicationContext _context = context;

        public async Task<AudienceModel?> GetAudienceByIdAsync(int id) {
            return await _context.Audiences.FirstOrDefaultAsync(model => model.Id == id);
        }

        public async Task<AudienceModel?> GetAudienceByNameAsync(string name) {
            return await _context.Audiences.FirstOrDefaultAsync(model => model.Name == name);
        }
        
        public async Task<List<AudienceModel>> GetAudiencesAsync(int offset, int limit) {
            return await _context.Audiences.OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }
        
        public async Task<List<AudienceModel>> SearchAudiencesAsync(string name, int offset, int limit) {
            return await _context.Audiences.Where(model => EF.Functions.Like(model.Name.ToLower(), $"%{name.ToLower()}%")).OrderBy(model => model.Id).Skip(offset).Take(limit).ToListAsync();
        }
        
        public async Task<AudienceModel> AddAudienceAsync(AudienceModel audienceModel) {
            var audience = await _context.Audiences.AddAsync(audienceModel);
            await _context.SaveChangesAsync();
            return audience.Entity;
        }
        
        public async Task<AudienceModel> RemoveAudienceAsync(AudienceModel audienceModel) {
            var audience = _context.Audiences.Remove(audienceModel);
            await _context.SaveChangesAsync();
            return audience.Entity;
        }
        
    }
}