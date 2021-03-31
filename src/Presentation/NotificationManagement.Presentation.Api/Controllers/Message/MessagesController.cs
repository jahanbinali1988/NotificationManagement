using System.Threading.Tasks;
using Common.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationManagement.Application.Service.Contract;
using NotificationManagement.Application.Dto;
using NotificationManagement.Presentation.Api.Messages.User.Request;

namespace NotificationManagement.Presentation.Api.Controllers.Messages
{
    public class MessagesController : BaseApiController
    {
        private readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService, ILogger<MessagesController> logger) : base(logger)
        {
            this._messageService = messageService;
        }

        [HttpPost]
        [Route("messages")]
        public async Task<ActionResult<bool>> SendMessage([FromBody] SendMessageRequest request)
        {
            var dto = request.Adapt<MessageDto>();

            await this._messageService.SendMessage(dto);

            return true;
        }

    }
}
