using Dapper;
using OvertimePolicies.Infrastructure.DbContexts;
using OvertimePolicies.SharedKernel.Interfaces;
using System.Data;

namespace OvertimePolicies.Infrastructure.Repositories.DapperRepositories
{
    public abstract class DapperRepositoryBase<T> : IDapperRepository<T> where T : class, IAggregateRoot
    {
        private readonly DapperDbContext _dapperDbContext;

        public DapperRepositoryBase(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        public async Task<IQueryable<T>> ExecuteCustomQueryAsync(string command, DynamicParameters? parms = null, CommandType commandType = CommandType.Text)
        {
            return await ExecuteCustomQueryAsync(command, parms, commandType, CancellationToken.None);
        }

        public async Task<IQueryable<T>> ExecuteCustomQueryAsync(string command, DynamicParameters? parms = null,
                CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            using (var connection = _dapperDbContext.CreateConnection())
            {
                IQueryable<T> result = (IQueryable<T>)await Task.Run(() => connection
                    .QueryAsync(command, parms, null, null, commandType), cancellationToken);
                return result;
            }
        }
    }
}