

using Factory.API.Data.Configurations;
using Factory.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }

    //Need this for Scaffold
    public class FactoryDbContextFatory : IDesignTimeDbContextFactory<FactoryDbContext>
    {
        public FactoryDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionalBuilder = new DbContextOptionsBuilder<FactoryDbContext>();
            var conn = config.GetConnectionString("FactoryDbConnectionString");
            optionalBuilder.UseSqlServer(conn);

            return new FactoryDbContext(optionalBuilder.Options);
        }
    }
}
