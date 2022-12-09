using Dapper;
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Infrastructure.DbContexts;
using OvertimePolicies.Services.Interfaces.DapperRepositories;

namespace OvertimePolicies.Infrastructure.Repositories.DapperRepositories
{
    public class DapperEmployeeRepository : DapperRepositoryBase<Employee>, IDapperEmployeeRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public DapperEmployeeRepository(DapperDbContext dapperDbContext) : base(dapperDbContext) => _dapperDbContext = dapperDbContext;

        public async Task<IQueryable<Employee>> GetAllEmployees()
        {
            var command = $"select Employee.EmployeeId, Employee.FirstName, Employee.LastName, Employee.EmploymentDate from Employee Where 1=1";
            using (var connection = _dapperDbContext.CreateConnection())
            {
                connection.Open();
                var employees = await connection.QueryAsync<Employee>(command);
                connection.Close();
                return employees.AsQueryable();
            }
        }
    }
}