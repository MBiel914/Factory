using Factory.API.Core.Models.Material;

namespace Factory.API.Core.Models.Tool
{
    public class ToolWithMaterialDto
        : BaseToolDto
    {
        public BaseMaterialDto Material { get; set; }
    }
}
