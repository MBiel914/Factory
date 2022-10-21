using Factory.API.Service.Models.Extras;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.Contracts
{
    public interface IGeneralRepository<TDbModel>
        where TDbModel : class
    {
        Task<TResult> GetAsync<TResult>(int? id)
            where TResult : class, IMapable<TDbModel, TResult>, new();

        Task<List<TResult>> GetAllAsync<TResult>()
            where TResult : class, IMapable<TDbModel, TResult>, new();
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters parameters)
            where TResult : class, IMapable<TDbModel, TResult>, new();

        Task<TResult> AddAsync<TSource, TResult>(TSource source)
            where TResult : class, IMapable<TSource, TResult>, new()
            where TSource : class;

        Task Delete(int id);

        Task Update<TSource>(int id, TSource source);

        Task<bool> Exists(int id);
    }
}
