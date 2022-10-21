using Factory.API.Service.Models.Tool;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.Models.ToolType
{
    public class ToolTypeDto : BaseToolTypeDto, IMapable<Data.Entities.ToolType, ToolTypeDto>
    {
        public virtual IList<ToolWithMaterialDto>? Tools { get; set; }

        public new ToolTypeDto Map(Data.Entities.ToolType source)
        {
            var result = new ToolTypeDto
            {
                Name = source.Name,
                Description = source.Description,
                Size = source.Size,
                SecondSize = source.SecondSize
            };

            if (source.Tools is null)
                return result;

            result.Tools = new List<ToolWithMaterialDto>();

            foreach (var item in source.Tools)
            {
                result.Tools.Add(new ToolWithMaterialDto().Map(item));
            }

            return result;
        }
    }
}
