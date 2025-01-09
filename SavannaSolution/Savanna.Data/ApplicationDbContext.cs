using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Savanna.Data.Data;
using Savanna.Data.Mapping;

namespace Savanna.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMappings());
        }

        async Task IApplicationDbContext.SaveChanges()
        {
            await base.SaveChangesAsync();
        }

        async Task IApplicationDbContext.Update<T>(T entity)
        {
            base.Update(entity);
        }
    }
}
