using Factory.API.Service.Contracts;
using Factory.API.Service.DTOs.ToolType;
using Factory.API.Data.Contexts;
using Factory.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Factory.API.Service.Repositories
{
    public class ToolTypeRepository : GeneralRepository<ToolTypeModel>, IToolTypeRepository
    {
        private readonly FactoryDbContext _context;

        public ToolTypeRepository(FactoryDbContext context)
            : base(context)
        {
            this._context = context;
        }

        public async Task<ToolTypeDto> GetDetails(int? id)
        {
            var result = await _context.ToolTypes
                .Include(item => item.Tools)
                .ThenInclude(item => item.Material)
                .FirstOrDefaultAsync(item => item.Id == id);

            return new ToolTypeDto().Map(result);
        }
    }
}
