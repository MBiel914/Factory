using Factory.API.Data.Models;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.DTOs.ToolType
{
    public class BaseToolTypeDto
        : IMapable<ToolTypeModel, BaseToolTypeDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Size { get; set; }
        public int? SecondSize { get; set; }

        public BaseToolTypeDto Map(ToolTypeModel source)
        {
            return new BaseToolTypeDto
            {
                Name = source.Name,
                Description = source.Description,
                Size = source.Size,
                SecondSize = source.SecondSize
            };
        }
    }
}
