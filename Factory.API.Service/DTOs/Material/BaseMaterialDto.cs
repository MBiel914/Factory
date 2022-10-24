using Factory.API.Data.Models;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.DTOs.Material
{
    public class BaseMaterialDto
        : IMapable<MaterialModel, BaseMaterialDto>
    {
        public string Name { get; set; }
        public int Quality { get; set; }
        public double CostPerKg { get; set; }

        public BaseMaterialDto Map(MaterialModel source)
        {
            return new BaseMaterialDto
            {
                Name = source.Name,
                Quality = source.Quality,
                CostPerKg = source.CostPerKg
            };
        }
    }
}
