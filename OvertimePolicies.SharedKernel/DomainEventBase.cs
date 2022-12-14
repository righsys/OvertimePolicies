using MediatR;

namespace OvertimePolicies.SharedKernel
{
    public abstract class DomainEventBase : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }
}