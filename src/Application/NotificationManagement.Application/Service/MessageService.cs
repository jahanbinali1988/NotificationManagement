using MongoDB.Driver;
using NotificationManagement.Application.Dto;
using NotificationManagement.Application.Service.Contract;
using NotificationManagement.Domain.Models.Message;
using NotificationManagement.Persistence.Mongo.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Application.Service
{
    public class MessageService : IMessageService
    {
        #region fields
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMessageRepository _messageRepository;
        #endregion

        #region ctor
        public MessageService(IMongoDatabase mongoDatabase, IMessageRepository messageRepository)
        {
            this._mongoDatabase = mongoDatabase;
            this._messageRepository = messageRepository;
        }
        #endregion

        #region fields
        public async Task SendMessage(MessageDto messageDto)
        {
            if (messageDto is null)
                throw new Exception("The message to send is null");

            foreach (var userId in messageDto.UserId)
            {
                var id = Guid.NewGuid();
                var message = await Message.Create(id, messageDto.Content, messageDto.Title, true, userId, new MessageValidator(this._mongoDatabase));

                await _messageRepository.Create(message);
            }
        }
        #endregion
    }
}
