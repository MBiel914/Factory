using AutoMapper;
using Factory.API.Core.Contracts;
using Factory.API.Core.Models.ToolType;
using Factory.API.Data.Contexts;
using Factory.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Factory.API.Core.Repositories
{
    public class ToolTypeRepository : GeneralRepository<ToolType>, IToolTypeRepository
    {
        private readonly FactoryDbContext _context;
        private readonly IMapper _mapper;

        public ToolTypeRepository(FactoryDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ToolTypeDto> GetDetails(int? id)
        {
            var result = await _context.ToolTypes
                .Include(item => item.Tools)
                .ThenInclude(item => item.Material)
                .FirstOrDefaultAsync(item => item.Id == id);

            return _mapper.Map<ToolTypeDto>(result);
        }
    }
}
