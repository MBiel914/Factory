using Factory.API.Core.Models.ToolType;
using Factory.API.Data.Entities;

namespace Factory.API.Core.Contracts
{
    public interface IToolTypeRepository : IGeneralRepository<ToolType>
    {
        Task<ToolTypeDto> GetDetails(int? id);
    }
}
