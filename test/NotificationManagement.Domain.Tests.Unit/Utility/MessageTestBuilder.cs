using NotificationManagement.Domain.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Domain.Tests.Unit.Utility
{
    public class MessageTestBuilder
    {
        #region MyRegion
        private Guid _id;
        private string _content;
        private string _title;
        private bool _isSent;
        private Guid _userId;
        private readonly IMessageValidator _messageValidator;
        #endregion

        #region ctor
        public MessageTestBuilder()
        {
            this._id = Guid.NewGuid();
            this._content = Guid.NewGuid().ToString();
            this._title = Guid.NewGuid().ToString();
            this._isSent = false;
            this._userId = Guid.NewGuid();

            this._messageValidator = Substitute.For<IMessageValidator>();
        }
        #endregion

        #region methods
        public MessageTestBuilder WithId(Guid id)
        {
            this._id = id;
            return this;
        }

        public MessageTestBuilder WithContent(string content)
        {
            this._content = content;
            return this;
        }

        public MessageTestBuilder WithTitle(string title)
        {
            this._title = title;
            return this;
        }

        public MessageTestBuilder WithSentStatus(bool isSent)
        {
            this._isSent = isSent;
            return this;
        }

        public MessageTestBuilder WithUserId(Guid userId)
        {
            this._userId = userId;
            return this;
        }

        public MessageTestBuilder IsContetntTooLong(bool returnValue)
        {
            this._messageValidator.IsTooLong(this._content).Returns(returnValue);
            return this;
        }

        public async Task<Models.Message.Message> Build()
        {
            return await Models.Message.Message.Create(this._id, this._content, this._title, this._isSent, this._userId, this._messageValidator);
        }
        #endregion
    }
}
