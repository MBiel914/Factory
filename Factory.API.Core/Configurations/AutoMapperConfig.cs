using AutoMapper;
using Factory.API.Core.Models.Material;
using Factory.API.Core.Models.Tool;
using Factory.API.Core.Models.ToolType;
using Factory.API.Data.Entities;

namespace Factory.API.Core.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ToolType, BaseToolTypeDto>().ReverseMap();
            CreateMap<ToolType, GetToolTypeDto>().ReverseMap();
            CreateMap<ToolType, ToolTypeDto>().ReverseMap();
         
            CreateMap<Tool, BaseToolDto>().ReverseMap();
            CreateMap<Tool, ToolWithMaterialDto>().ReverseMap();

            CreateMap<Material, BaseMaterialDto>().ReverseMap();
        }
    }
}
