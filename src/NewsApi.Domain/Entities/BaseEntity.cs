using System;

namespace NewsApi.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity() =>
            (Id, RegistrationAt) = (Guid.NewGuid(), DateTime.Now);
        protected BaseEntity(Guid id) =>
            (Id, RegistrationAt) = (id, DateTime.Now);

        public virtual Guid Id { get; set; }
        public virtual DateTime RegistrationAt { get; private set; }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as BaseEntity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity a, BaseEntity b)
        {
            return !(a == b);
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id={Id}]";
    }
}