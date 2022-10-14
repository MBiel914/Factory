using Factory.API.Core.Models.Extras;

namespace Factory.API.Core.Contracts
{
    public interface IGeneralRepository<T>
        where T : class
    {
        Task<TResult> GetAsync<TResult>(int? id);
        
        Task<List<TResult>> GetAllAsync<TResult>();
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters parameters);

        Task<TResult> AddAsync<TSource, TResult>(TSource source);

        Task DeleteAsyc(int id);

        Task UpdateAsync<TSource>(int id, TSource source);

        Task<bool> Exists(int id);
    }
}
