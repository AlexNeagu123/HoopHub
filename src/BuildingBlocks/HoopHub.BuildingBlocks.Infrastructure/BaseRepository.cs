using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.BuildingBlocks.Infrastructure
{
    public class BaseRepository<T> (DbContext context) : IAsyncRepository<T>
        where T : class
    {
        protected readonly DbContext Context = context;

        public async Task<Result<T>> FindByIdAsync(string id)
        {
            var result = await Context.Set<T>().FindAsync(id);
            return result == null ? Result<T>.Failure($"Entity with Id {id} not found") : Result<T>.Success(result);
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public async Task<Result<T>> DeleteAsync(Guid id)
        {
            var result = await FindByIdAsync(id);
            if (!result.IsSuccess)
            {
                return Result<T>.Failure($"Entity with Id {id} not found");
            }
            Context.Set<T>().Remove(result.Value);
            await Context.SaveChangesAsync();
            return Result<T>.Success(result.Value);
        }

        public virtual Result<IReadOnlyList<T>> GetAll()
        {
            return Result<IReadOnlyList<T>>.Success(Context.Set<T>().ToList());
        }

        public virtual async Task<Result<T>> FindByIdAsync(Guid id)
        {
            var result = await Context.Set<T>().FindAsync(id);
            return result == null ? Result<T>.Failure($"Entity with Id {id} not found") : Result<T>.Success(result);
        }
        public async Task<Result<T>> UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }
    }
}
