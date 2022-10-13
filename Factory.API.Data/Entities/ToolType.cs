namespace Factory.API.Data.Entities
{
    public class ToolType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Size { get; set; }
        public int? SecondSize { get; set; }

        public virtual IList<Tool>? Tools { get; set; }
    }
}
