using Factory.API.Service.DTOs.Tool;
using Factory.API.Service.Configurations;
using Factory.API.Data.Models;

namespace Factory.API.Service.DTOs.ToolType
{
    public class ToolTypeDto : BaseToolTypeDto, IMapable<ToolTypeModel, ToolTypeDto>
    {
        public virtual IList<ToolWithMaterialDto>? Tools { get; set; }

        public new ToolTypeDto Map(ToolTypeModel source)
        {
            BaseToolTypeDto baseToolType = base.Map(source);
            ToolTypeDto result = new ToolTypeDto
            {
                Name = baseToolType.Name,
                Description = baseToolType.Description,
                Size = baseToolType.Size,
                SecondSize = baseToolType.SecondSize
            };

            if (source.Tools is null)
            {
                return result;
            }

            result.Tools = new List<ToolWithMaterialDto>();

            foreach (var item in source.Tools)
            {
                result.Tools.Add(new ToolWithMaterialDto().Map(item));
            }

            return result;
        }
    }
}
