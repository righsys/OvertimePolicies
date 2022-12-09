using OvertimePolicies.Domain.DbViews;
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Infrastructure.DbContexts;
using OvertimePolicies.Services.Interfaces.DapperRepositories;
using OvertimePolicies.Services.DTOs;
using Dapper;
using OvertimePolicies.Services.Mappers;

namespace OvertimePolicies.Infrastructure.Repositories.DapperRepositories
{
    public class DapperEmployeeSalaryRepository : DapperRepositoryBase<EmployeeSalary>, IDapperEmployeeSalaryRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public DapperEmployeeSalaryRepository(DapperDbContext dapperDbContext) : base(dapperDbContext) => _dapperDbContext = dapperDbContext;

        public async Task<IEnumerable<EmployeeSalaryDbView>> GetALlEmployeeSalariesByEmployeeId(int employeeId)
        {
            var command = $"Select EmployeeSalary.EmployeeSalaryId, " +
            $"EmployeeSalary.EmployeeId, EmployeeSalary.Year,EmployeeSalary.Month,EmployeeSalary.Salary,EmployeeSalary.BasicSalary, " +
            $"EmployeeSalary.Allowance,EmployeeSalary.Transportation,EmployeeSalary.Overtime,EmployeeSalary.Tax " +
            $"from Employee inner join EmployeeSalary on Employee.EmployeeId=EmployeeSalary.EmployeeId " +
            $"where EmployeeSalary.EmployeeId=@Id " +
            $"order by EmployeeSalary.Year desc, EmployeeSalary.Month desc";
            using (var connection = _dapperDbContext.CreateConnection())
            {
                connection.Open();
                IEnumerable<EmployeeSalaryDbView> result = await connection.QueryAsync<EmployeeSalaryDbView>(command, new { Id = employeeId });
                connection.Close();
                return result;
            }
        }
        public async Task<EmployeeSalaryDbView> GetEmployeeSalaryByMonth(int employeeId,int year, int monthNumber)
        {
            var command = $"Select EmployeeSalary.EmployeeSalaryId, " +
            $"EmployeeSalary.EmployeeId, EmployeeSalary.Year,EmployeeSalary.Month,EmployeeSalary.Salary,EmployeeSalary.BasicSalary, " +
            $"EmployeeSalary.Allowance,EmployeeSalary.Transportation,EmployeeSalary.Overtime,EmployeeSalary.Tax " +
            $"from Employee inner join EmployeeSalary on Employee.EmployeeId=EmployeeSalary.EmployeeId " +
            $"where EmployeeSalary.EmployeeId=@Id and EmployeeSalary.Month=@Month and EmployeeSalary.Year=@Year" +
            $" and 1=1";
            using (var connection = _dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<EmployeeSalaryDbView>(command, new { Id = employeeId, Month = monthNumber, Year = year });
                connection.Close();
                return result;
            }
        }
        public async Task<IEnumerable<EmployeeSalaryDbView>> GetAllEmployeeSalariesByDateRange(int employeeId, int startYear, int endYear, int startMonth, int endMonth)
        {
            var command = $"Select EmployeeSalary.EmployeeSalaryId, " +
           $"EmployeeSalary.EmployeeId, EmployeeSalary.Year,EmployeeSalary.Month,EmployeeSalary.Salary,EmployeeSalary.BasicSalary, " +
           $"EmployeeSalary.Allowance,EmployeeSalary.Transportation,EmployeeSalary.Overtime,EmployeeSalary.Tax " +
           $"from Employee inner join EmployeeSalary on Employee.EmployeeId=EmployeeSalary.EmployeeId " +
           $"where EmployeeSalary.EmployeeId=@Id and (EmployeeSalary.Year BETWEEN @StartYear and @EndYear ) and (EmployeeSalary.Month BETWEEN @StartMonth and @EndMonth) " +
           $"and 1=1";
            using (var connection = _dapperDbContext.CreateConnection())
            {
                connection.Open();
                IEnumerable<EmployeeSalaryDbView> result = await connection
                    .QueryAsync<EmployeeSalaryDbView>(command, new { Id = employeeId, StartYear = startYear, EndYear = endYear, StartMonth = startMonth, EndMonth = endMonth });
                connection.Close();
                return result;
            }
        }
    }
}