using Factory.API.Service.Configurations;

namespace Factory.API.Core.Models.ToolType
{
    public class GetToolTypeDto
        : BaseToolTypeDto, IMapable<Data.Entities.ToolType, GetToolTypeDto>, IMapable<BaseToolTypeDto, GetToolTypeDto>
    {
        public int Id { get; set; }

        public GetToolTypeDto()
        {
        }

        private GetToolTypeDto(BaseToolTypeDto baseToolTypeDto)
        {
            Name = baseToolTypeDto.Name;
            Description = baseToolTypeDto.Description;
            Size = baseToolTypeDto.Size;
            SecondSize = baseToolTypeDto.SecondSize;
        }

        public new GetToolTypeDto Map(Data.Entities.ToolType source)
        {
            var result = new GetToolTypeDto(base.Map(source));
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
    }
}
