using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.API.Data.Models
{
    public class ToolModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Margin { get; set; }

        [ForeignKey(nameof(ToolTypeId))]
        public int ToolTypeId { get; set; }
        public ToolTypeModel ToolType { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public int MaterialId { get; set; }
        public MaterialModel Material { get; set; }
    }
}
