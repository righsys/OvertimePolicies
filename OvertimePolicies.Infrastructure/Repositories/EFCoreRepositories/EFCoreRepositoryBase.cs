using Microsoft.EntityFrameworkCore;
using OvertimePolicies.SharedKernel.Interfaces;

namespace OvertimePolicies.Infrastructure.Repositories.EFCoreRepositories
{
    public abstract class EFCoreRepositoryBase<T, EntityKey> : IEFCoreRepository<T, EntityKey> where T : class, IAggregateRoot
    {
        private readonly DbContext _dbContext;

        public EFCoreRepositoryBase(DbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public abstract Task<IQueryable<T>> GetObjectSet();
        //
        // Add
        //
        public Task<T> AddAsync(T entity)
        {
            return AddAsync(entity, CancellationToken.None);
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
        //
        // Delete
        //
        public Task DeleteAsync(EntityKey Id)
        {
            return DeleteAsync(Id, CancellationToken.None);
        }
        public async Task DeleteAsync(EntityKey Id, CancellationToken cancellationToken)
        {
            var entity = await FindByKeyAsync(Id);
            _dbContext.Set<T>().Entry(entity).State = EntityState.Deleted;
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        //
        // Update
        //
        public Task UpdateAsync(T entity)
        {
            return UpdateAsync(entity, CancellationToken.None);
        }
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        //
        // Exist
        //
        public Task<bool> ExistAsync(EntityKey Id)
        {
            return ExistAsync(Id, CancellationToken.None);
        }
        public async Task<bool> ExistAsync(EntityKey Id, CancellationToken cancellationToken) => await FindByKeyAsync(Id) != null;
        //
        // Find By Key
        //
        public Task<T> FindByKeyAsync(EntityKey Id)
        {
            return FindByKeyAsync(Id, CancellationToken.None);
        }
        public async Task<T> FindByKeyAsync(EntityKey Id, CancellationToken cancellationToken) => await _dbContext.Set<T>().FindAsync(Id);
        //
        // Get All
        //
        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await GetAllAsync(CancellationToken.None);
        }
        public async Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken) => await GetObjectSet();
    }
}