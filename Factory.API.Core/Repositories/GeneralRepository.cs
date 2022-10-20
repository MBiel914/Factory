using Factory.API.Core.Contracts;
using Factory.API.Core.Exceptions;
using Factory.API.Core.Models.Extras;
using Factory.API.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Factory.API.Core.Repositories
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
            where TResult : class, IMapable<TSource, TResult>, new()
            where TSource : class
        {
            var entity = source.GenericMapper<TSource, TDbModel>();

            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();

            return entity.GenericMapper<TDbModel, TResult>();
        }

        public Task Delete(int id)
        {
            var entity = _context.Set<TDbModel>().Find(id);

            if (entity is null)
                throw new NotFoundException(typeof(TDbModel).Name, id);

            _context.Set<TDbModel>().Remove(entity);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task<bool> Exists(int id)
        {
            var entity = _context.Set<TDbModel>().Find(id);
            return Task.FromResult(entity != null);
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

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters parameters)
            where TResult : class, IMapable<TDbModel, TResult>, new()
        {
            var totalSize = await _context.Set<TDbModel>().CountAsync();
            var newElements = await _context.Set<TDbModel>()
                .Skip(parameters.StartIndex)
                .Take(parameters.PageSize)
                .ToListAsync();

            var result = new PagedResult<TResult>
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
                throw new NotFoundException(typeof(TDbModel).Name, id.HasValue ? id : "No Key Provided");

            return new TResult().Map(result);
        }

        public Task Update<TSource>(int id, TSource source)
        {
            throw new NotImplementedException();
            var entity = source.GenericMapper<TSource, TDbModel>();
            entity = _context.Set<TDbModel>().Find(id);

            if (entity is null)
                throw new NotFoundException(typeof(TDbModel).Name, id);

            _context.Set<TDbModel>().Update(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
