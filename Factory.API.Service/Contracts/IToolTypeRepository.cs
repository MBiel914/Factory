using Factory.API.Service.DTOs.ToolType;
using Factory.API.Data.Models;

namespace Factory.API.Service.Contracts
{
    public interface IToolTypeRepository : IGeneralRepository<ToolTypeModel>
    {
        Task<ToolTypeDto> GetDetails(int? id);
    }
}
