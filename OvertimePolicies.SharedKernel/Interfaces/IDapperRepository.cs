using Dapper;
using System.Data;

namespace OvertimePolicies.SharedKernel.Interfaces
{
    public interface IDapperRepository<T> where T : class, IAggregateRoot
    {
        //Task<T> FindByKeyAsync(TId Id);
        //Task<T> FindByKeyAsync(TId Id, CancellationToken cancellationToken);
        Task<IQueryable<T>> ExecuteCustomQueryAsync(string command, DynamicParameters parms, CommandType commandType = CommandType.Text);
        Task<IQueryable<T>> ExecuteCustomQueryAsync(string command, DynamicParameters parms, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);
    }
}
