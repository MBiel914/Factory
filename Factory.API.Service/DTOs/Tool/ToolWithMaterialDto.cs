using Factory.API.Service.DTOs.Material;
using Factory.API.Service.Configurations;
using Factory.API.Data.Models;
using Factory.API.Service.DTOs.ToolType;

namespace Factory.API.Service.DTOs.Tool
{
    public record ToolWithMaterialDto
        : BaseToolDto, IMapable<ToolModel, ToolWithMaterialDto>
    {
        public BaseMaterialDto Material { get; init; }

        public ToolWithMaterialDto Map(ToolModel source)
        {
            BaseToolDto baseToolType = base.Map(source);
            return new ToolWithMaterialDto
            {
                Name = baseToolType.Name,
                Margin = baseToolType.Margin,
                Weight = baseToolType.Weight,
                Material = new BaseMaterialDto().Map(source.Material)
            };
        }
    }
}
