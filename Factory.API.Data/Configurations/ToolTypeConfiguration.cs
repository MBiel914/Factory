using Factory.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.API.Data.Configurations
{
    public class ToolTypeConfiguration : IEntityTypeConfiguration<ToolTypeModel>
    {
        public void Configure(EntityTypeBuilder<ToolTypeModel> builder)
        {
            builder.HasData(
                new ToolTypeModel
                {
                    Id = 1,
                    Name = "Klucz płaski 13",
                    Size = 13,
                    SecondSize = 12
                },
                new ToolTypeModel
                {
                    Id = 2,
                    Name = "Klucz płaski 14",
                    Size = 14,
                    SecondSize = 13
                },
                new ToolTypeModel
                {
                    Id = 3,
                    Name = "Klucz płaski 15",
                    Size = 15,
                    SecondSize = 14
                },
                new ToolTypeModel
                {
                    Id = 4,
                    Name = "Klucz płaski 16",
                    Size = 16,
                    SecondSize = 15
                },
                new ToolTypeModel
                {
                    Id = 5,
                    Name = "Klucz francuski 13",
                    Description = "Klucz francuski 13 desc",
                    Size = 13
                },
                new ToolTypeModel
                {
                    Id = 6,
                    Name = "Klucz francuski 16",
                    Description = "Klucz francuski 16 desc",
                    Size = 16
                },
                new ToolTypeModel
                {
                    Id = 7,
                    Name = "Klucz francuski 17",
                    Description = "Klucz francuski 17 desc",
                    Size = 17
                },
                new ToolTypeModel
                {
                    Id = 8,
                    Name = "Klucz francuski 18",
                    Description = "Klucz francuski 18 desc",
                    Size = 18
                }
                );
        }
    }
}
