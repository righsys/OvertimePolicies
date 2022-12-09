using MediatR;
using Microsoft.AspNetCore.Mvc;
using OvertimePolicies.Api.DTOs;
using OvertimePolicies.Services.Commands.Employee.AddEmployee;
using OvertimePolicies.Services.DTOs;
using OvertimePolicies.Services.Queries.GetEmployeeList;
using OvertimePolicies.WebApp.Common.DatetimeHelper;

namespace OvertimePolicies.Api.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public readonly IDateTimeHelper _dateTimeHelper;
        public EmployeeController(IMediator mediator, IDateTimeHelper dateTimeHelper)
        {
            _mediator = mediator;
            _dateTimeHelper = dateTimeHelper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEmployees()
        {
            GetEmployeeListQuery query = new GetEmployeeListQuery() { };
            GetEmployeeListQueryResponse response = await _mediator.Send(query);
            if (response.Success)
                return Ok(response.Employees);
            return StatusCode(StatusCodes.Status500InternalServerError, "خطا در پردازش اطلاعات ورودی");
        }
        [HttpPost("Add")]
        public async Task<IActionResult> GetEmployeeById([FromBody] EmployeeForCreateDto employee)
        {
            AddEmployeeCommand command = new AddEmployeeCommand()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmploymentDate = _dateTimeHelper.GetLocalDateTime()
            };
            AddEmployeeCommandResponse response = await _mediator.Send(command);
            if (response.Success)
                return Ok("ثبت اطلاعات موفقیت آمیز بود");
            return StatusCode(StatusCodes.Status500InternalServerError, "خطا در پردازش اطلاعات ورودی");
        }
    }
}
