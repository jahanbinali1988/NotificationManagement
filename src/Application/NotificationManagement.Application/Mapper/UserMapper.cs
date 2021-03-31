using NotificationManagement.Application.Dto;
using NotificationManagement.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationManagement.Application.Mapper
{
    public static class UserMapper
    {
        public static UserDto Map(this User user)
        {
            if (user == null)
                return new UserDto();

            return new UserDto()
            {
                BirthDate = user.BirthDate,
                Email = user.Email,
                Family = user.Family,
                Id = user.Id,
                IsActive = user.IsActive,
                IsMarrid = user.IsMarrid,
                Mobile = user.Mobile,
                Name = user.Name,
                Sex = user.Sex
            };
        }
    }
}
