﻿using Factory.API.Service.Configurations;

namespace Factory.API.Service.Models.Material
{
    public class BaseMaterialDto
        : IMapable<Data.Entities.Material, BaseMaterialDto>
    {
        public string Name { get; set; }
        public int Quality { get; set; }
        public double CostPerKg { get; set; }

        public BaseMaterialDto Map(Data.Entities.Material source)
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