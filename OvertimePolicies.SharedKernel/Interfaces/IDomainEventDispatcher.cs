namespace OvertimePolicies.SharedKernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAndClearEvents(IEnumerable<EntityBase<int>> entitiesWithEvents);
    }
}
