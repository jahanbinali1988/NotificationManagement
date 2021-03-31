using System;

namespace Common.Domain
{
    public abstract class BaseEntity : IEntity
    {
        protected bool Equals(BaseEntity other)
        {
            return Id.Equals(other.Id) && CreatedAt.Equals(other.CreatedAt) && Nullable.Equals(ModifiedAt, other.ModifiedAt) 
                   && Nullable.Equals(DeletedAt, other.DeletedAt) && IsDeleted == other.IsDeleted;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseEntity) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CreatedAt, ModifiedAt, DeletedAt, IsDeleted);
        }

        protected BaseEntity()
        {
            this.CreatedAt = DateTimeOffset.Now;
        }
        public Guid Id { get; set; }
        /// <summary>
        /// Creation date and time of this entity
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Modification date and time of this entity
        /// </summary>
        public DateTimeOffset? ModifiedAt { get; set; }

        /// <summary>
        /// Deletion date and time of this entity
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }

        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Soft delete. Indicates if this entity is deleted or not.
        /// </summary>

        public void Delete()
        {
            this.IsDeleted = true;
            this.DeletedAt = DateTimeOffset.Now;
        }
        public void MarkAsUpdated()
        {
            this.ModifiedAt = DateTimeOffset.Now;
        }

    }
}