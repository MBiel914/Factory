using Factory.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.API.Data.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasData(
                new Material
                {
                    Id = 1,
                    Name = "Stal nierdzewna 9%",
                    Quality = 1,
                    CostPerKg = 4.60
                },
                new Material
                {
                    Id = 2,
                    Name = "Stal nierdzewna 8%",
                    Quality = 2,
                    CostPerKg = 4.50
                },
                new Material
                {
                    Id = 3,
                    Name = "Stal nierdzewna wióry",
                    Quality = 3,
                    CostPerKg = 3.00
                }
                );
        }
    }
}
