using AutoMapper;
using AutoMapper.QueryableExtensions;
using Factory.API.Core.Contracts;
using Factory.API.Core.Exceptions;
using Factory.API.Core.Models.Extras;
using Factory.API.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Factory.API.Core.Repositories
{
    public class GeneralRepository<TDbModel> : IGeneralRepository<TDbModel>
        where TDbModel : class
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
            var entity = _mapper.Map<TDbModel>(source);

            _context.AddRange(entity);
            _context.SaveChanges();

            return _mapper.Map<TResult>(entity);
        }

        public Task Delete(int id)
        {
            var entity = GetAsync<TDbModel>(id);

            if(entity is null)
                throw new NotFoundException(typeof(TDbModel).Name, id);

            _context.Set<TDbModel>().Remove(entity.GetAwaiter().GetResult());
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task<bool> Exists(int id)
        {
            var entity = GetAsync<TDbModel>(id);
            return Task.FromResult(entity != null);
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            return await _context.Set<TDbModel>()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters parameters)
        {
            var totalSize = await _context.Set<TDbModel>().CountAsync();
            var result = await _context.Set<TDbModel>()
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
            var result = await _context.Set<TDbModel>().FindAsync(id);

            if (result is null)
                throw new NotFoundException(typeof(TDbModel).Name, id.HasValue ? id : "No Key Provided");

            return _mapper.Map<TResult>(result);
        }

        public Task Update<TSource>(int id, TSource source)
        {
            var entity = GetAsync<TDbModel>(id);

            if (entity is null)
                throw new NotFoundException(typeof(TDbModel).Name, id);

            _mapper.Map(source, entity);
            _context.Update(entity);

            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
