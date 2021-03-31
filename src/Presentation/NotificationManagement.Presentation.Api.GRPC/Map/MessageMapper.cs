using NotificationManagement.Application.Dto;
using NotificationManagement.Presentation.Api.GRPC.Proto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationManagement.Presentation.Api.GRPC.Map
{
    public static class MessageMapper
    {
        public static MessageDto Map(Message message)
        {
            if (message == null)
                return new MessageDto();

            var dto = new MessageDto() { UserId = new List<Guid>() };
            dto.Content = message.Content;
            dto.Title = message.Title;
            dto.UserId.AddRange(message.UserIds.Where(c=> Guid.TryParse(c, out var id)).Select(s=> Guid.Parse(s)).ToList());

            return dto;
        }
    }
}
