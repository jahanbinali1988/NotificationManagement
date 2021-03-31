using NotificationManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationManagement.Application.Service.Contract
{
    public interface IUserService
    {
        Task AddUser(UserDto request);
        Task<UserDto> Get(Guid id);
    }
}
