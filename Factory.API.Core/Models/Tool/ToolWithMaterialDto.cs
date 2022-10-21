using Factory.API.Core.Models.Material;
using Factory.API.Service.Configurations;

namespace Factory.API.Core.Models.Tool
{
    public class ToolWithMaterialDto
        : BaseToolDto, IMapable<Data.Entities.Tool, ToolWithMaterialDto>
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

        public new ToolWithMaterialDto Map(Data.Entities.Tool source)
        {
            var result = new ToolWithMaterialDto(base.Map(source));
            result.Material = new BaseMaterialDto().Map(source.Material);

            return result;
        }
    }
}
