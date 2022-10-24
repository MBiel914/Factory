using Factory.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.API.Data.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<MaterialModel>
    {
        public void Configure(EntityTypeBuilder<MaterialModel> builder)
        {
            builder.HasData(
                new MaterialModel
                {
                    Id = 1,
                    Name = "Stal nierdzewna 9%",
                    Quality = 1,
                    CostPerKg = 4.60
                },
                new MaterialModel
                {
                    Id = 2,
                    Name = "Stal nierdzewna 8%",
                    Quality = 2,
                    CostPerKg = 4.50
                },
                new MaterialModel
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
