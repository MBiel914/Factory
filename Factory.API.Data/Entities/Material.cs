namespace Factory.API.Data.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quality { get; set; }
        public double CostPerKg { get; set; }

        public virtual IList<Tool>? Tools { get; set; }
    }
}
