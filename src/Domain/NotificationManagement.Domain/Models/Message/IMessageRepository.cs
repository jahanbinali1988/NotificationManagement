using Common.Domain;
using System;
using System.Threading.Tasks;

namespace NotificationManagement.Domain.Models.Message
{
    public interface IMessageRepository : IRepository<Guid, Message>
    {
    }
}
