using OvertimePolicies.Domain.Entities;
using OvertimePolicies.SharedKernel;

namespace OvertimePolicies.Domain.Events
{
    public class EmployeeAddedEvent : DomainEventBase
    {
        public Employee Employee { get; set; }
        public EmployeeAddedEvent(Employee employee)
        {
            this.Employee = employee;
        }
    }
}