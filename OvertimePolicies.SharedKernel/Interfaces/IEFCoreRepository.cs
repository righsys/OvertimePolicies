namespace OvertimePolicies.SharedKernel.Interfaces
{
    public interface IEFCoreRepository<T, TId> where T : class, IAggregateRoot
    {
        //
        // Queries
        //
        Task<T> FindByKeyAsync(TId Id);
        Task<T> FindByKeyAsync(TId Id, CancellationToken cancellationToken);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistAsync(TId Id);
        Task<bool> ExistAsync(TId Id, CancellationToken cancellationToken);
        //
        // Commands
        //
        Task DeleteAsync(TId Id);
        Task DeleteAsync(TId Id, CancellationToken cancellationToken);
        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    }
}
