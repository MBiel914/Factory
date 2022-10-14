using AutoMapper;
using AutoMapper.QueryableExtensions;
using Factory.API.Core.Contracts;
using Factory.API.Core.Models.Extras;
using Factory.API.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Factory.API.Core.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T>
        where T : class
    {
        private readonly FactoryDbContext _context;
        private readonly IMapper _mapper;

        public GeneralRepository(FactoryDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Task<TResult> AddAsync<TSource, TResult>(TSource source)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsyc(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync<T>(id);
            return entity != null;
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            return await _context.Set<T>()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public Task<List<TResult>> GetAllAsync<TResult>(QueryParameters parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<TResult> GetAsync<TResult>(int? id)
        {
            if (id is null)
                throw new ArgumentNullException();

            var result = await _context.Set<T>().FindAsync(id);
            return _mapper.Map<TResult>(result);
        }

        public Task UpdateAsync<TSource>(int id, TSource source)
        {
            throw new NotImplementedException();
        }
    }
}
