using Grpc.Core;
using Mapster;
using NotificationManagement.Application.Service.Contract;
using NotificationManagement.Presentation.Api.GRPC.Map;
using NotificationManagement.Presentation.Api.GRPC.Proto;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Presentation.Api.GRPC.GrpcService
{
    public class NotificationService : NotoficationManagementService.NotoficationManagementServiceBase
    {
        #region fields
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        #endregion

        #region ctor
        public NotificationService(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }
        #endregion

        #region methods
        public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.UserId, out var userId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The given userId is invalid"));

            var userDto = await _userService.Get(userId);
            var user = userDto.Map();

            return new GetUserResponse() { User = user };
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.User.Email))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The given Email is invalid"));

            if (string.IsNullOrEmpty(request.User.Mobile))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The given Mobile is invalid"));

            var userDto = request.User.Map();

            await _userService.AddUser(userDto);

            return new CreateUserResponse() { IsSuccessfull = true };
        }

        public override async Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
        {
            if (!request.Message.UserIds.Any())
                throw new RpcException(new Status(StatusCode.InvalidArgument, "The given Email is invalid"));

            var messageDto = MessageMapper.Map(request.Message);
            await _messageService.SendMessage(messageDto);

            return new SendMessageResponse() { IsSuccessfull = true };
        }
        #endregion
    }
}
