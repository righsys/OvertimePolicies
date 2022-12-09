using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using OvertimePolicies.Api.Commands;
using OvertimePolicies.Api.DTOs;
using OvertimePolicies.Api.Services;
using OvertimePolicies.Domain.Enums;
using OvertimePolicies.Services.Commands.EmployeeSalary.AddEmployeeSalary;
using OvertimePolicies.Services.Commands.EmployeeSalary.DeleteEmployeeSalary;
using OvertimePolicies.Services.Commands.EmployeeSalary.UpdateEmployeeSalary;
using OvertimePolicies.Services.Interfaces;
using OvertimePolicies.Services.Queries.GetEmployeeSalaries;
using OvertimePolicies.Services.Queries.GetEmployeeSalaryByMonth;
using OvertimePolicies.Services.Queries.GetEmployeeSalaryByMonthRange;
using OvertimePolicies.WebApp.Common.DatetimeHelper;

namespace OvertimePolicies.Api.Controllers
{
    [ApiController]
    [Route("api/Salary")]
    public class SalaryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOvertimeCalculatorMethods _overtimeCalculatorMethods;
        private readonly IDateTimeHelper _dateTimeHelper;
        private List<int> Years = new List<int>();

        public SalaryController(IMediator mediator,
            IOvertimeCalculatorMethods overtimeCalculatorMethods,
            IDateTimeHelper dateTimeHelper)
        {
            _mediator = mediator;
            _overtimeCalculatorMethods = overtimeCalculatorMethods;
            _dateTimeHelper = dateTimeHelper;
            Years.Add(1400);
            Years.Add(1401);
        }

        //
        // Get all employee's salaries
        //
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetSalaries(int employeeId)
        {
            GetEmployeeSalariesQueryResponse response = new GetEmployeeSalariesQueryResponse();
            try
            {
                GetEmployeeSalariesQuery query = new GetEmployeeSalariesQuery() { EmployeeId = employeeId };
                response = await _mediator.Send(query);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, response.CustomErrorMessage);
                if (response.EmployeeSalaries.Any())
                    return Ok(response.EmployeeSalaries);
                return NotFound("اطلاعاتی یافت نشد");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.CustomErrorMessage);
            }

        }

        //
        // Get employee salary by month
        //
        [HttpGet("GetMonth")]
        public async Task<IActionResult> GetSalary(int employeeId, MonthNames month, int year = 1401)
        {
            GetEmployeeSalaryByMonthQueryResponse response = new GetEmployeeSalaryByMonthQueryResponse();
            try
            {
                GetEmployeeSalaryByMonthQuery query = new GetEmployeeSalaryByMonthQuery() { EmployeeId = employeeId, Month = (int)month, Year = year };
                response = await _mediator.Send(query);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, response.CustomErrorMessage);
                if (response.EmployeeSalary != null)
                    return Ok(response.EmployeeSalary);
                return NotFound("اطلاعاتی یافت نشد");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        //
        // Get employee salary by range
        //
        [HttpGet("GetRange")]
        public async Task<IActionResult> GetRange(int employeeId, int StartMonth, int EndMonth, int StartYear = 1401 , int EndYear = 1401)
        {
            GetEmployeeSalaryByMonthRangeQuery getSalaryByRange = new GetEmployeeSalaryByMonthRangeQuery() 
            {
                EndMonth = EndMonth,
                StartMonth = StartMonth,    
                StartYear = StartYear,  
                EndYear = EndYear,
                EmployeeId = employeeId,
            };
            GetEmployeeSalaryByMonthRangeQueryResponse response = await _mediator.Send(getSalaryByRange);
            return Ok(response.EmployeeSalaries);
        }


        //
        // Add new salary b custom data
        //
        [HttpPost("Custom/AddData")]
        public async Task<IActionResult> AddSalary([FromBody] EmployeeSalaryForUpsertDto salary)
        {
            AddEmployeeSalaryCommandResponse response = new AddEmployeeSalaryCommandResponse();
            try
            {
                CreateUpsertEmployeSalaryCommand commandCreator = new CreateUpsertEmployeSalaryCommand(_overtimeCalculatorMethods);
                UpsertEmployeSalaryCommand upsertCommand = commandCreator.GetCommandForUpsertEmployeeSalary(salary, out bool Success, out string ErrorMessage);
                if (upsertCommand is null)
                    return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessage);

                AddEmployeeSalaryCommand command = new AddEmployeeSalaryCommand()
                {
                    Allowance = upsertCommand.Allowance,
                    BasicSalary = upsertCommand.BasicSalary,
                    FirstName = upsertCommand.FirstName,
                    LastName = upsertCommand.LastName,
                    Month = upsertCommand.Month,
                    OverTime = upsertCommand.OverTime,
                    Salary = upsertCommand.Salary,
                    Tax = upsertCommand.Tax,
                    Transportation = upsertCommand.Transportation,
                    Year = upsertCommand.Year
                };
                response = await _mediator.Send(command);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, response.CustomErrorMessage);
                return Ok("اطلاعات با موفقیت ذخیره شد");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "خطا در پردازش اطلاعات ورودی");
            }
        }

        //
        // Update employee salary by custom data
        //
        [HttpPut("Custom/UpdateData")]
        public async Task<IActionResult> UpdateSalary([FromBody] EmployeeSalaryForUpsertDto salary)
        {
            UpdateEmployeeSalaryCommandResponse response = new UpdateEmployeeSalaryCommandResponse();
            try
            {
                CreateUpsertEmployeSalaryCommand commandCreator = new CreateUpsertEmployeSalaryCommand(_overtimeCalculatorMethods);
                UpsertEmployeSalaryCommand upsertCommand = commandCreator.GetCommandForUpsertEmployeeSalary(salary, out bool Success, out string ErrorMessage);
                if (upsertCommand is null)
                    return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessage);

                UpdateEmployeeSalaryCommand command = new UpdateEmployeeSalaryCommand()
                {
                    Allowance = upsertCommand.Allowance,
                    BasicSalary = upsertCommand.BasicSalary,
                    FirstName = upsertCommand.FirstName,
                    LastName = upsertCommand.LastName,
                    Month = upsertCommand.Month,
                    OverTime = upsertCommand.OverTime,
                    Salary = upsertCommand.Salary,
                    Tax = upsertCommand.Tax,
                    Transportation = upsertCommand.Transportation,
                    Year = upsertCommand.Year
                };
                response = await _mediator.Send(command);
                if (!response.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, response.CustomErrorMessage);
                return Ok("اطلاعات با موفقیت ذخیره شد");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "خطا در پردازش اطلاعات ورودی");
            }
        }

        //
        // CSV data manager
        //
        [HttpPost("CSV/AddData")]
        public IActionResult AddCsvSalary(IFormFile csvfile) 
        {
            return Ok("عدم پیاده سازی");
        }
        [HttpPut("CSV/UpdateData")]
        public IActionResult UpdateCsvSalary(IFormFile csvfile)
        {
            return Ok("عدم پیاده سازی");
        }

        //
        // xml data manager
        //
        [HttpPost("Xml/AddData")]
        public IActionResult AddXmlSalary(IFormFile xmlfile)
        {
            return Ok("عدم پیاده سازی");
        }
        [HttpPut("Xml/UpdateData")]
        public IActionResult UpdateXmlSalary(IFormFile xmlfile)
        {
            return Ok("عدم پیاده سازی");
        }

        //
        // json data manager
        //
        [HttpPost("Json/AddData")]
        public IActionResult AddJsonSalary(IFormFile xmlfile)
        {
            return Ok("عدم پیاده سازی");
        }
        [HttpPut("Json/UpdateData")]
        public IActionResult UpdateJsonSalary(IFormFile xmlfile)
        {
            return Ok("عدم پیاده سازی");
        }


        //
        // Delete employee salary
        //
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteSalary(string firstName, string lastName, int month, int year = 1401)
        {
            DeleteEmployeeSalaryCommandResponse response = new DeleteEmployeeSalaryCommandResponse();
            try
            {
                DeleteEmployeeSalaryCommand command = new DeleteEmployeeSalaryCommand()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Month = month,
                    Year = year
                };
                response = await _mediator.Send(command);

                if (!response.Success)
                    return StatusCode(StatusCodes.Status500InternalServerError, response.CustomErrorMessage);
                return Ok("اطلاعات با موفقیت حذف شد");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "خطا در پردازش اطلاعات ورودی");
            }
        }

    }
}
