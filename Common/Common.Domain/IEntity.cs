using System;

namespace Common.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }

        /// <summary>
        /// Creation date and time of this entity
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Modification date and time of this entity
        /// </summary>
        DateTimeOffset? ModifiedAt { get; set; }

        /// <summary>
        /// Deletion date and time of this entity
        /// </summary>
        DateTimeOffset? DeletedAt { get; set; }
        /// <summary>
        /// Soft delete. Indicates if this entity is deleted or not.
        /// </summary>
        bool IsDeleted { get; }

        void MarkAsUpdated();
    }
}
