using FluentAssertions;
using NotificationManagement.Domain.Models.Message;
using NotificationManagement.Domain.Tests.Unit.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NotificationManagement.Domain.Tests.Unit.User
{
    public class MessageTests
    {
        [Fact]
        public async Task The_Message_Constructor_properlyAsync()
        {
            var message = await new MessageTestBuilder()
                .WithId(MessageTestData.Id)
                .WithContent(MessageTestData.Content)
                .WithTitle(MessageTestData.Title)
                .WithSentStatus(MessageTestData.IsSent)
                .WithUserId(MessageTestData.UserId)
                .Build();

            message.Id.Should().Be(MessageTestData.Id);
            message.Content.Should().Be(MessageTestData.Content);
            message.Title.Should().Be(MessageTestData.Title);
            message.IsSent.Should().BeTrue();
            message.UserId.Should().Be(MessageTestData.UserId);
        }

        [Fact]
        public void The_Message_construct_occur_error_if_the_content_is_too_long()
        {
            Func<Task<Message>> message = async () => await new MessageTestBuilder()
                .WithContent(MessageTestData.TooLongContent)
                .IsContetntTooLong(true)
                .Build();

            message.Should().Throw<Exceptions.TooLongMessageException>();
        }
    }
}
