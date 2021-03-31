using Common.Domain;
using System;

namespace NotificationManagement.Domain.Models.User
{
    public interface IUserRepository : IRepository<Guid, User>
    {

    }
}
