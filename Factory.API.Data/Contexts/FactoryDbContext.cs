using Factory.API.Data.Configurations;
using Factory.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Factory.API.Data.Contexts
{
    public class FactoryDbContext : IdentityDbContext<IdentityUser>
    {
        public FactoryDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<ToolType> ToolTypes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Tool> Tools { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ToolTypeConfiguration());
            builder.ApplyConfiguration(new MaterialConfiguration());
            builder.ApplyConfiguration(new ToolConfiguration());
        }
    }
}
