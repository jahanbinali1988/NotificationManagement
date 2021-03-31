using System;
using System.Collections.Generic;

namespace NotificationManagement.Application.Dto
{
    public class MessageDto
    {
        public List<Guid> UserId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}
