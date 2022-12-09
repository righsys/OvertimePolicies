namespace OvertimePolicies.Api.DTOs
{
    public class EmployeeSalaryForUpsertDto
    {
        public CustomSalaryDataDto SalaryData { get; set; }
        public string OverTimeCalculator { get; set; }
    }
}
