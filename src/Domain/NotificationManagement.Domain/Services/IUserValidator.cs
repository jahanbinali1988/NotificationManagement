using System.Threading.Tasks;

namespace NotificationManagement.Domain.Services
{
    public interface IUserValidator
    {
        Task<bool> IsDuplicateMobile(string mobile);
        Task<bool> IsDuplicateEmail(string email);
    }
}
