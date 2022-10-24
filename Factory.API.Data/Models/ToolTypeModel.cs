namespace Factory.API.Data.Models
{
    public class ToolTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Size { get; set; }
        public int? SecondSize { get; set; }

        public virtual IList<ToolModel>? Tools { get; set; }
    }
}
