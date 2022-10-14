using AutoMapper;
using AutoMapper.QueryableExtensions;
using Factory.API.Core.Contracts;
using Factory.API.Core.Exceptions;
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

        public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
        {
            var entity = _mapper.Map<T>(source);

            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TResult>(entity);
        }

        public async Task DeleteAsyc(int id)
        {
            var entity = await GetAsync<T>(id);

            if(entity is null)
                throw new NotFoundException(typeof(T).Name, id);

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
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

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters parameters)
        {
            var totalSize = await _context.Set<T>().CountAsync();
            var result = await _context.Set<T>()
                .Skip(parameters.StartIndex)
                .Take(parameters.PageSize)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<TResult>
            {
                TotalCount = totalSize,
                PageSize = parameters.PageSize,
                Items = result
            };
        }

        public async Task<TResult> GetAsync<TResult>(int? id)
        {
            var result = await _context.Set<T>().FindAsync(id);

            if (result is null)
                throw new NotFoundException(typeof(T).Name, id.HasValue ? id : "No Key Provided");

            return _mapper.Map<TResult>(result);
        }

        public async Task UpdateAsync<TSource>(int id, TSource source)
        {
            var entity = await GetAsync<T>(id);

            if (entity is null)
                throw new NotFoundException(typeof(T).Name, id);

            _mapper.Map(source, entity);
            _context.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
