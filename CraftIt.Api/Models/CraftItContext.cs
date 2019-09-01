using Microsoft.EntityFrameworkCore;

namespace CraftIt.Api.Models 
{
    public class CraftItContext : DbContext
    {
        public CraftItContext(DbContextOptions<CraftItContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}