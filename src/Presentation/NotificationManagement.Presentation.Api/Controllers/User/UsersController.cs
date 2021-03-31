using System;
using System.Threading.Tasks;
using Common.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationManagement.Application.Dto;
using NotificationManagement.Application.Service.Contract;
using NotificationManagement.Presentation.Api.Messages.Message.Request;

namespace NotificationManagement.Presentation.Api.Controllers.Users
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ILogger<UsersController> logger) : base(logger)
        {
            this._userService = userService;
        }

        [HttpPost("users")]
        public async Task<ActionResult<bool>> CreateUser([FromBody] CreateUserRequest request)
        {
            var dto = request.Adapt<UserDto>();

            await _userService.AddUser(dto);

            return true;
        }

        [HttpGet("users/{id}")]
        public async Task<UserDto> GetUser(Guid id)
        {
            return await _userService.Get(id);
        }
    }
}
