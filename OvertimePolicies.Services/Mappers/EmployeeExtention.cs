using OvertimePolicies.Domain.DbViews;
using OvertimePolicies.Domain.Entities;
using OvertimePolicies.Services.DTOs;

namespace OvertimePolicies.Services.Mappers
{
    public static class EmployeeExtention
    {
        //
        // Employee
        //
        public static EmployeeDto ConvertToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                EmployeeId = employee.EmployeeId,
                EmploymentDate = employee.EmploymentDate,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            };
        }
        public static List<EmployeeDto> ConvertToEmployeeDtos(this IEnumerable<Employee> employees)
        {
            var result = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                result.Add(employee.ConvertToEmployeeDto());
            }
            return result;
        }
        //
        // Employee Salary
        //
        public static EmployeeSalaryDto ConvertToEmployeeSalaryDto(this EmployeeSalary salary)
        {
            return new EmployeeSalaryDto()
            {
                Allowance = salary.Allowance,
                BasicSalary = salary.BasicSalary,
                EmployeeSalaryId = salary.EmployeeSalaryId,
                Month = salary.Month,
                Salary = salary.Salary,
                Transportation = salary.Transportation,
                Year = salary.Year,
                Overtime = salary.Overtime,
                EmployeeId = salary.EmployeeId,
                Tax = salary.Tax,
            };
        }
        public static List<EmployeeSalaryDto> ConvertToEmployeeSalaryDtos(this IEnumerable<EmployeeSalary> employeeSalaries)
        {
            var result = new List<EmployeeSalaryDto>();
            foreach (var salary in employeeSalaries)
            {
                result.Add(salary.ConvertToEmployeeSalaryDto());
            }
            return result;
        }
        public static List<EmployeeSalaryDto> ConvertToEmployeeSalaryDtos(this List<EmployeeSalary> employeeSalaries)
        {
            var result = new List<EmployeeSalaryDto>();
            foreach (var salary in employeeSalaries)
            {
                result.Add(salary.ConvertToEmployeeSalaryDto());
            }
            return result;
        }
        public static EmployeeSalaryDto ConvertToEmployeeSalaryDto(this EmployeeSalaryDbView salary)
        {
            return new EmployeeSalaryDto()
            {
                Allowance = salary.Allowance,
                BasicSalary = salary.BasicSalary,
                EmployeeSalaryId = salary.EmployeeSalaryId,
                Month = salary.Month,
                Salary = salary.Salary,
                Transportation = salary.Transportation,
                Year = salary.Year,
                Overtime = salary.Overtime,
                EmployeeId = salary.EmployeeId,
                Tax = salary.Tax,
            };
        }
        public static List<EmployeeSalaryDto> ConvertToEmployeeSalaryDtos(this IEnumerable<EmployeeSalaryDbView> employeeSalaries)
        {
            var result = new List<EmployeeSalaryDto>();
            foreach (var salary in employeeSalaries)
            {
                result.Add(salary.ConvertToEmployeeSalaryDto());
            }
            return result;
        }
        public static EmployeeSalary ConvertToEmployeeSalary(this EmployeeSalaryDbView salary) 
        {
            return new EmployeeSalary() 
            {
                Allowance=salary.Allowance,
                BasicSalary=salary.BasicSalary,
                EmployeeId=salary.EmployeeId,
                EmployeeSalaryId = salary.EmployeeSalaryId,
                Month = salary.Month,
                Salary=salary.Salary,
                Overtime=salary.Overtime,   
                Tax = salary.Tax,
                Transportation = salary.Transportation,
                Year = salary.Year,
            };
        }
    }
}