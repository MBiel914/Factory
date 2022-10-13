using AutoMapper;
using Factory.API.Core.Contracts;
using Factory.API.Data.Contexts;
using Factory.API.Data.Entities;

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
    }
}
