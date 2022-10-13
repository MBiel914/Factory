using AutoMapper;
using Factory.API.Core.Contracts;
using Factory.API.Core.Models.Extras;
using Factory.API.Data.Contexts;

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

        public Task<List<TResult>> GetAllAsync<TResult>()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> GetAllAsync<TResult>(QueryParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> GetAsync<TResult>(int? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<TSource>(int id, TSource source)
        {
            throw new NotImplementedException();
        }
    }
}
