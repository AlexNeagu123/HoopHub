using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.BuildingBlocks.Application.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<Result<T>> FindByIdAsync(Guid id);
        Task<Result<T>> FindByIdAsync(string id);

        Task<Result<T>> AddAsync(T entity);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result<T>> DeleteAsync(Guid id);
        Result<IReadOnlyList<T>> GetAll();
    }
}
