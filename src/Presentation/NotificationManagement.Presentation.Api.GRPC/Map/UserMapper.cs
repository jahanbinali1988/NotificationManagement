using Google.Protobuf.WellKnownTypes;
using NotificationManagement.Application.Dto;
using NotificationManagement.Presentation.Api.GRPC.Proto;
using System;

namespace NotificationManagement.Presentation.Api.GRPC.Map
{
    public static class UserMapper
    {
        public static User Map(this UserDto dto)
        {
            if (dto == null || dto.Id == Guid.Empty)
                return new User();

            var user = new User();
            user.Email = dto.Email;
            user.Family = dto.Family;
            user.IsActive = dto.IsActive;
            user.IsMarrid = dto.IsMarrid;
            user.Mobile = dto.Mobile;
            user.Name = dto.Name;
            user.Sex = dto.Sex;
            user.BirthDate = dto.BirthDate.ToTimestamp();

            return user;
        }

        public static UserDto Map(this User user)
        {
            if (user == null)
                return new UserDto();

            var dto = new UserDto();
            dto.Email = user.Email;
            dto.Family = user.Family;
            dto.IsActive = user.IsActive;
            dto.IsMarrid = user.IsMarrid;
            dto.Mobile = user.Mobile;
            dto.Name = user.Name;
            dto.Sex = user.Sex;
            dto.BirthDate = user.BirthDate.ToDateTime();

            return dto;
        }
    }
}
