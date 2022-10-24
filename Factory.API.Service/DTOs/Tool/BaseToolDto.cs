using Factory.API.Data.Models;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.DTOs.Tool
{
    public class BaseToolDto : IMapable<ToolModel, BaseToolDto>
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Margin { get; set; }

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
