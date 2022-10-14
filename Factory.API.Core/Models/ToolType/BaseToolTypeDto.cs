namespace Factory.API.Core.Models.ToolType
{
    public class BaseToolTypeDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Size { get; set; }
        public int? SecondSize { get; set; }
    }
}
