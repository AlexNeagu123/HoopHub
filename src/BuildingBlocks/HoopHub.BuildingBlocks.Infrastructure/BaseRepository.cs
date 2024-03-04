using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.BuildingBlocks.Infrastructure
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly NBADataContext context;
        public BaseRepository(NBADataContext context)
        {
            this.context = context;
        }
        public async Task<Result<T>> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public async Task<Result<T>> DeleteAsync(Guid id)
        {
            var result = await FindByIdAsync(id);
            if (!result.IsSuccess)
            {
                return Result<T>.Failure($"Entity with Id {id} not found");
            }
            context.Set<T>().Remove(result.Value);
            await context.SaveChangesAsync();
            return Result<T>.Success(result.Value);
        }

        public virtual Result<IReadOnlyList<T>> GetAll()
        {
            return Result<IReadOnlyList<T>>.Success(context.Set<T>().ToList());
        }

        public virtual async Task<Result<T>> FindByIdAsync(Guid id)
        {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null)
            {
                return Result<T>.Failure($"Entity with Id {id} not found");
            }
            return Result<T>.Success(result);
        }
        public async Task<Result<T>> UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

    }
}
