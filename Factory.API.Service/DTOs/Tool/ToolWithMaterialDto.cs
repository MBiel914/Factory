using Factory.API.Service.DTOs.Material;
using Factory.API.Service.Configurations;
using Factory.API.Data.Models;
using Factory.API.Service.DTOs.ToolType;

namespace Factory.API.Service.DTOs.Tool
{
    public class ToolWithMaterialDto
        : BaseToolDto, IMapable<ToolModel, ToolWithMaterialDto>
    {
        public BaseMaterialDto Material { get; set; }

        public new ToolWithMaterialDto Map(ToolModel source)
        {
            BaseToolDto baseToolType = base.Map(source);
            ToolWithMaterialDto result = new ToolWithMaterialDto
            {
                Name = baseToolType.Name,
                Margin = baseToolType.Margin,
                Weight = baseToolType.Weight
            };

            result.Material = new BaseMaterialDto().Map(source.Material);

            return result;
        }
    }
}
