using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Contexts {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ClassModel> Classes { get; set; }
        public DbSet<AudienceModel> Audiences { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
    }
}
