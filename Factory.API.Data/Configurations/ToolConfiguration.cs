using Factory.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.API.Data.Configurations
{
    internal class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.HasData(
                new Tool
                {
                    Id = 1,
                    Weight = 0.070,
                    Margin = 0.10,
                    ToolTypeId = 1,
                    MaterialId = 1
                },
                new Tool
                {
                    Id = 2,
                    Weight = 0.070,
                    Margin = 0.80,
                    ToolTypeId = 1,
                    MaterialId = 2
                },
                new Tool
                {
                    Id = 3,
                    Weight = 0.070,
                    Margin = 0.60,
                    ToolTypeId = 1,
                    MaterialId = 3
                }
                );
        }
    }
}
