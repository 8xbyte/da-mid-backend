using Microsoft.EntityFrameworkCore;

namespace DaMid.Contexts {
    public class ApplicationContext : DbContext {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
