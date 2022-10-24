using Factory.API.Service.Contracts;
using Factory.API.Service.Exceptions;
using Factory.API.Service.DTOs.Extras;
using Factory.API.Data.Contexts;
using Factory.API.Service.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Factory.API.Service.Repositories
{
    public class GeneralRepository<TDbModel> : IGeneralRepository<TDbModel>
        where TDbModel : class, new()
    {
        private readonly FactoryDbContext _context;

        public GeneralRepository(FactoryDbContext context)
        {
            this._context = context;
        }

        public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
            where TResult : class, IMapable<TSource, TResult>, IMapable<TDbModel, TResult>, new()
            where TSource : class, IMapable<TSource, TDbModel>
        {
            var entity = source.Map(source);

            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();

            return new TResult().Map(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<TDbModel>().FindAsync(id);

            if (entity is null)
            {
                throw new NotFoundException(typeof(TDbModel).Name, id);
            }

            _context.Set<TDbModel>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _context.Set<TDbModel>().FindAsync(id);
            return entity != null;
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
            where TResult : class, IMapable<TDbModel, TResult>, new()
        {
            var result = new List<TResult>();
            var newElements = await _context.Set<TDbModel>().ToListAsync();

            foreach (var element in newElements)
            {
                result.Add(new TResult().Map(element));
            }

            return result;
        }

        public async Task<PagedResultDto<TResult>> GetAllAsync<TResult>(QueryParametersDto parameters)
            where TResult : class, IMapable<TDbModel, TResult>, new()
        {
            var totalSize = await _context.Set<TDbModel>().CountAsync();
            var newElements = await _context.Set<TDbModel>()
                .Skip(parameters.StartIndex)
                .Take(parameters.PageSize)
                .ToListAsync();

            var result = new PagedResultDto<TResult>
            {
                TotalCount = totalSize,
                PageSize = parameters.PageSize
            };


            result.Items = new List<TResult>();

            foreach (var element in newElements)
            {
                result.Items.Add(new TResult().Map(element));
            }

            return result;
        }

        public async Task<TResult> GetAsync<TResult>(int? id)
            where TResult : class, IMapable<TDbModel, TResult>, new()
        {
            var result = await _context.Set<TDbModel>().FindAsync(id);

            if (result is null)
            {
                throw new NotFoundException(typeof(TDbModel).Name, id.HasValue ? id : "No Key Provided");
            }

            return new TResult().Map(result);
        }

        public async Task UpdateAsync<TSource>(int id, TSource source)
            where TSource : class, IMapable<TSource, TDbModel>
        {
            var entity = await _context.Set<TDbModel>().FindAsync(id);

            if (entity is null)
            {
                throw new NotFoundException(typeof(TDbModel).Name, id);
            }

            _context.Entry(entity).State = EntityState.Detached;

            entity = source.Map(source);

            _context.Set<TDbModel>().Update(entity);
            _context.SaveChanges();
        }
    }
}
