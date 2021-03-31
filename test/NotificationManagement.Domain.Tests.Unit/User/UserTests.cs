using Xunit;
using NotificationManagement.Domain.Tests.Unit.Utility;
using System.Threading.Tasks;
using FluentAssertions;
using System;

namespace NotificationManagement.Domain.Tests.Unit.User
{
    public class UserTests
    {
        [Fact]
        public async Task The_User_Constructor_properlyAsync()
        {
            var user = await new UserTestBuilder()
                .WithId(UserTestData.Id)
                .WithName(UserTestData.Name)
                .WithFamily(UserTestData.Family)
                .WithSex(UserTestData.Sex)
                .WithIsActive(UserTestData.IsActive)
                .WithIsMarried(UserTestData.IsMarrid)
                .WithEmail(UserTestData.Email)
                .WithMobile(UserTestData.Mobile)
                .WithBirthDate(UserTestData.BirthDate)
                .Build();

            user.Name.Should().Be(UserTestData.Name);
            user.Family.Should().Be(UserTestData.Family);
            user.Sex.Should().BeTrue();
            user.IsMarrid.Should().BeTrue();
            user.IsActive.Should().BeTrue();
            user.Mobile.Should().Be(UserTestData.Mobile);
            user.Email.Should().Be(UserTestData.Email);
            user.BirthDate.Should().Be(UserTestData.BirthDate);
        }

        [Fact]
        public void The_user_construct_occur_error_if_the_email_is_duplicated()
        {
            Func<Task<Models.User.User>> user = async () => await new UserTestBuilder()
                .WithEmail(UserTestData.Email)
                .IsEmailDuplicate(true)
                .Build();

            user.Should().Throw<Exceptions.UserEmailDuplicateException>();
        }

        [Fact]
        public void The_user_construct_occur_error_if_the_mobile_is_duplicated()
        {
            Func<Task<Models.User.User>> user = async () => await new UserTestBuilder()
                .WithMobile(UserTestData.Mobile)
                .IsMobileDuplicate(true)
                .Build();

            user.Should().Throw<Exceptions.UserMobileDuplicateException>();
        }
    }
}
