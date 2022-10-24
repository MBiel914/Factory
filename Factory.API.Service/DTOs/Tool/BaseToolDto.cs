using Factory.API.Data.Models;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.DTOs.Tool
{
    public record BaseToolDto : IMapable<ToolModel, BaseToolDto>
    {
        public string Name { get; init; }
        public double Weight { get; init; }
        public double Margin { get; init; }

        public BaseToolDto Map(ToolModel source)
        {
            return new BaseToolDto
            {
                Name = source.Name,
                Weight = source.Weight,
                Margin = source.Margin
            };
        }
    }
}
