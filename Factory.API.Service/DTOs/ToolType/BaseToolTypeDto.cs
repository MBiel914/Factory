using Factory.API.Data.Models;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.DTOs.ToolType
{
    public record BaseToolTypeDto
        : IMapable<ToolTypeModel, BaseToolTypeDto>, IMapable<BaseToolTypeDto, ToolTypeModel>
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public int Size { get; init; }
        public int? SecondSize { get; init; }

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

        public ToolTypeModel Map(BaseToolTypeDto source)
        {
            return new ToolTypeModel
            {
                Name = source.Name,
                Description = source.Description,
                Size = source.Size,
                SecondSize = source.SecondSize
            };
        }
    }
}
