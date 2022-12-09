using System.ComponentModel.DataAnnotations.Schema;

namespace OvertimePolicies.SharedKernel
{
    public abstract class EntityBase<TId> : IEquatable<EntityBase<TId>>
    {
        [NotMapped]
        public TId Id { get; set; }
        //
        // Overrides and Operators
        //
        public bool Equals(EntityBase<TId> otherEntity)
        {
            if (otherEntity is null)
                return false;
            if (otherEntity.GetType() != typeof(EntityBase<TId>))
                return false;
            return true;
        }
        public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
        {
            return EqualityComparer<EntityBase<TId>>.Default.Equals(left, right);
        }
        public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
        {
            return !(left == right);
        }
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() != typeof(EntityBase<TId>))
                return false;
            if (obj is not EntityBase<TId> entity)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        //
        // Domain Event Functionality
        //
        private List<DomainEventBase> _domainEvents = new();
        [NotMapped]
        public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
        public void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
        internal void ClearDomainEvents() => _domainEvents.Clear();
    }
}