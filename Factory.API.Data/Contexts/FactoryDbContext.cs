using Factory.API.Data.Configurations;
using Factory.API.Data.Models;
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
            : base(options)
        {
        }

        public DbSet<ToolTypeModel> ToolTypes { get; set; }
        public DbSet<MaterialModel> Materials { get; set; }
        public DbSet<ToolModel> Tools { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ToolTypeConfiguration());
            builder.ApplyConfiguration(new MaterialConfiguration());
            builder.ApplyConfiguration(new ToolConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }

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
