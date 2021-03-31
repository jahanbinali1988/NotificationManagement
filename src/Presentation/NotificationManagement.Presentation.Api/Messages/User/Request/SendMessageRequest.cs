using System;
using System.Collections.Generic;

namespace NotificationManagement.Presentation.Api.Messages.User.Request
{
    public class SendMessageRequest
    {
        public IEnumerable<Guid> UserId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}
