using DaMid.Models;
using Microsoft.EntityFrameworkCore;

namespace DaMid.Contexts {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
    }
}
