using Factory.API.Service.DTOs.Material;
using Factory.API.Service.Configurations;
using Factory.API.Data.Models;

namespace Factory.API.Service.DTOs.Tool
{
    public class ToolWithMaterialDto
        : BaseToolDto, IMapable<ToolModel, ToolWithMaterialDto>
    {
        public BaseMaterialDto Material { get; set; }

        public ToolWithMaterialDto()
        {
        }

        private ToolWithMaterialDto(BaseToolDto baseTool)
        {
            Name = baseTool.Name;
            Weight = baseTool.Weight;
            Margin = baseTool.Margin;
        }

        public new ToolWithMaterialDto Map(ToolModel source)
        {
            var result = new ToolWithMaterialDto(base.Map(source));
            result.Material = new BaseMaterialDto().Map(source.Material);

            return result;
        }
    }
}
