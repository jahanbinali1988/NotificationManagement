using NotificationManagement.Application.Dto;
using System.Threading.Tasks;

namespace NotificationManagement.Application.Service.Contract
{
    public interface IMessageService
    {
        public Task SendMessage(MessageDto message);
    }
}
