using Factory.API.Service.DTOs.Tool;
using Factory.API.Service.Configurations;
using Factory.API.Data.Models;

namespace Factory.API.Service.DTOs.ToolType
{
    public record ToolTypeDto : BaseToolTypeDto, IMapable<ToolTypeModel, ToolTypeDto>
    {
        public virtual IList<ToolWithMaterialDto>? Tools { get; init; }

        public ToolTypeDto Map(ToolTypeModel source)
        {
            BaseToolTypeDto baseToolType = base.Map(source);
            ToolTypeDto result = new ToolTypeDto
            {
                Name = baseToolType.Name,
                Description = baseToolType.Description,
                Size = baseToolType.Size,
                SecondSize = baseToolType.SecondSize,
                Tools = GetToolsIList(source.Tools)
            };

            return result;
        }

        private IList<ToolWithMaterialDto>? GetToolsIList(IList<ToolModel>? source)
        {
            if (source is null)
            {
                return null;
            }

            var result = new List<ToolWithMaterialDto>();

            foreach (var item in source)
            {
                result.Add(new ToolWithMaterialDto().Map(item));
            }

            return result;
        }
    }
}
