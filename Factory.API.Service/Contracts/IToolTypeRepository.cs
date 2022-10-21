using Factory.API.Service.Models.ToolType;
using Factory.API.Data.Entities;

namespace Factory.API.Service.Contracts
{
    public interface IToolTypeRepository : IGeneralRepository<ToolType>
    {
        Task<ToolTypeDto> GetDetails(int? id);
    }
}
