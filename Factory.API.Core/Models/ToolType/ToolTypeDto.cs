using Factory.API.Core.Models.Tool;
using Factory.API.Data.Entities;

namespace Factory.API.Core.Models.ToolType
{
    public class ToolTypeDto : BaseToolTypeDto
    {
        public virtual IList<ToolWithMaterialDto>? Tools { get; set; }
    }
}
