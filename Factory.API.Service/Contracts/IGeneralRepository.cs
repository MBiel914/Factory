using Factory.API.Service.DTOs.Extras;
using Factory.API.Service.Configurations;

namespace Factory.API.Service.Contracts
{
    public interface IGeneralRepository<TDbModel>
        where TDbModel : class, new()
    {
        Task<TResult> GetAsync<TResult>(int? id)
            where TResult : class, IMapable<TDbModel, TResult>, new();

        Task<List<TResult>> GetAllAsync<TResult>()
            where TResult : class, IMapable<TDbModel, TResult>, new();
        Task<PagedResultDto<TResult>> GetAllAsync<TResult>(QueryParametersDto parameters)
            where TResult : class, IMapable<TDbModel, TResult>, new();

        Task<TResult> AddAsync<TSource, TResult>(TSource source)
            where TResult : class, IMapable<TSource, TResult>, IMapable<TDbModel, TResult>, new()
            where TSource : class, IMapable<TSource, TDbModel>;

        Task DeleteAsync(int id);

        Task UpdateAsync<TSource>(int id, TSource source)
            where TSource : class, IMapable<TSource, TDbModel>;

        Task<bool> ExistsAsync(int id);
    }
}
