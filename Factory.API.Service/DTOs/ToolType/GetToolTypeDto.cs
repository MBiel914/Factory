using Factory.API.Data.Models;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.DTOs.ToolType
{
    public class GetToolTypeDto
        : BaseToolTypeDto, IMapable<ToolTypeModel, GetToolTypeDto>
        , IMapable<BaseToolTypeDto, GetToolTypeDto>, IMapable<GetToolTypeDto, ToolTypeModel>
    {
        public int Id { get; set; }

        public new GetToolTypeDto Map(ToolTypeModel source)
        {
            BaseToolTypeDto baseToolType = base.Map(source);
            GetToolTypeDto result = new GetToolTypeDto
            {
                Name = baseToolType.Name,
                Description = baseToolType.Description,
                Size = baseToolType.Size,
                SecondSize = baseToolType.SecondSize
            };

            result.Id = source.Id;

            return result;
        }

        public GetToolTypeDto Map(BaseToolTypeDto source)
        {
            return new GetToolTypeDto
            {
                Name = source.Name,
                Description = source.Description,
                Size = source.Size,
                SecondSize = source.SecondSize
            };
        }

        public ToolTypeModel Map(GetToolTypeDto source)
        {
            return new ToolTypeModel
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Size = source.Size,
                SecondSize = source.SecondSize
            };
        }
    }
}
