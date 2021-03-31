using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Domain.Services
{
    public interface IMessageValidator
    {
        Task<bool> IsTooLong(string content);
    }
}
