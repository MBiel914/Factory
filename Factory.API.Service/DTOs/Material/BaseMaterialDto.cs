using Factory.API.Data.Models;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.DTOs.Material
{
    public record BaseMaterialDto
        : IMapable<MaterialModel, BaseMaterialDto>
    {
        public string Name { get; init; }
        public int Quality { get; init; }
        public double CostPerKg { get; init; }

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
