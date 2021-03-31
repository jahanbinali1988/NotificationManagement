using Common.Domain;
using NotificationManagement.Domain.Exceptions;
using NotificationManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Domain.Models.Message
{
    public class Message : AggregateRoot
    {
        #region fields
        public string Content { get; set; }
        public string Title { get; set; }
        public bool IsSent { get; set; }
        public Guid UserId { get; set; }
        #endregion

        #region ctor
        private Message()
        {

        }

        private Message(Guid id, string content, string title, bool isSent, Guid userId)
        {
            this.Id = id;
            this.Content = content;
            this.Title = title;
            this.CreatedAt = DateTimeOffset.Now;
            this.IsSent = isSent;
            this.UserId = userId;
        }
        #endregion

        #region method
        public static async Task<Message> Create(Guid id, string content, string title, bool isSent, Guid userId, IMessageValidator messageValidator)
        {
            var isMessageTooLong = await messageValidator.IsTooLong(content);

            GuardAgainstTooLongMessageContentDuplicate(isMessageTooLong);

            return new Message(id, content, title, isSent, userId);
        }

        private static void GuardAgainstTooLongMessageContentDuplicate(bool isDuplicate)
        {
            if (isDuplicate)
                throw new TooLongMessageException();
        }
        #endregion
    }
}
