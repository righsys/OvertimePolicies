namespace OvertimePolicies.Domain.DbViews
{
    public class EmployeeSalaryDbView
    {
        public int EmployeeSalaryId { get; set; }
        public int EmployeeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Salary { get; set; }
        public int BasicSalary { get; set; }
        public int Allowance { get; set; }
        public int Transportation { get; set; }
        public int Overtime { get; set; }
        public int Tax { get; set; }
    }
}