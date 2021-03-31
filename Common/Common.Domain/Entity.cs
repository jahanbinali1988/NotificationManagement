using System;
using System.Collections.Generic;

namespace Common.Domain
{
    public abstract class Entity : BaseEntity, IEquatable<Entity>
    {
        public bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SeqId == other.SeqId &&
                   Id.Equals(other.Id) &&
                   Equals(Version, other.Version) &&
                   CreatedAt.Equals(other.CreatedAt) &&
                   Nullable.Equals(ModifiedAt, other.ModifiedAt) &&
                   Nullable.Equals(DeletedAt, other.DeletedAt) &&
                   IsDeleted == other.IsDeleted;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Entity)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SeqId, Id, Version, CreatedAt, ModifiedAt, DeletedAt, IsDeleted);
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Auto incrementing clustering Id if required by the storage
        /// </summary>
        public long SeqId { get; protected set; }

        /// <summary>
        /// Row version
        /// </summary>
        public byte[] Version { get; private set; }


        /// <summary>
        /// Creates a shallow copy of this entity
        /// </summary>
        /// <returns></returns>
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
    }
}