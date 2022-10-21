using Factory.API.Service.Configurations;

namespace Factory.API.Service.Models.ToolType
{
    public class BaseToolTypeDto
        : IMapable<Data.Entities.ToolType, BaseToolTypeDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Size { get; set; }
        public int? SecondSize { get; set; }

        public BaseToolTypeDto Map(Data.Entities.ToolType source)
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
