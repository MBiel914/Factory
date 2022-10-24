namespace Factory.API.Data.Models
{
    public class MaterialModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quality { get; set; }
        public double CostPerKg { get; set; }

        public virtual IList<ToolModel>? Tools { get; set; }
    }
}
