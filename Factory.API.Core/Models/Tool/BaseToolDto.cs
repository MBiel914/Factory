using Factory.API.Service.Configurations;

namespace Factory.API.Core.Models.Tool
{
    public class BaseToolDto : IMapable<Data.Entities.Tool, BaseToolDto>
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Margin { get; set; }

        public BaseToolDto Map(Data.Entities.Tool source)
        {
            return new BaseToolDto
            {
                Name = source.Name,
                Weight = source.Weight,
                Margin = source.Margin
            };
        }
    }
}
