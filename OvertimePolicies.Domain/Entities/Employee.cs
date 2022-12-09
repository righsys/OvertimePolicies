using OvertimePolicies.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace OvertimePolicies.Domain.Entities
{
    public class Employee : AuditableEntity
    {
        public Employee()
        {
            EmployeeSalaries = new HashSet<EmployeeSalary>();
        }
        //
        // Properties
        //
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EmploymentDate { get; set; }
        
        //
        //Just for soft delete purpose
        //
        //public bool IsDeleted { get; set; }

        //
        // Navigation Properties
        //
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}