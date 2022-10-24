using Factory.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.API.Data.Configurations
{
    internal class ToolConfiguration : IEntityTypeConfiguration<ToolModel>
    {
        public void Configure(EntityTypeBuilder<ToolModel> builder)
        {
            builder.HasData(
                new ToolModel
                {
                    Id = 1,
                    Name = "Klucz płaski 13 (Stal nierdzewna 9%)",
                    Weight = 0.070,
                    Margin = 0.10,
                    ToolTypeId = 1,
                    MaterialId = 1
                },
                new ToolModel
                {
                    Id = 2,
                    Name = "Klucz płaski 13 (Stal nierdzewna 8%)",
                    Weight = 0.070,
                    Margin = 0.80,
                    ToolTypeId = 1,
                    MaterialId = 2
                },
                new ToolModel
                {
                    Id = 3,
                    Name = "Klucz płaski 13 (Stal nierdzewna wióry)",
                    Weight = 0.070,
                    Margin = 0.60,
                    ToolTypeId = 1,
                    MaterialId = 3
                }
                );
        }
    }
}
