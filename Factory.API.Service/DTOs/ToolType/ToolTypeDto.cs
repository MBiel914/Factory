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
            var result = new ToolTypeDto
            {
                Name = source.Name,
                Description = source.Description,
                Size = source.Size,
                SecondSize = source.SecondSize
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
