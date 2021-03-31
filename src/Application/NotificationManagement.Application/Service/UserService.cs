using MongoDB.Driver;
using NotificationManagement.Application.Dto;
using NotificationManagement.Application.Mapper;
using NotificationManagement.Application.Service.Contract;
using NotificationManagement.Domain.Models.User;
using NotificationManagement.Persistence.Mongo.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Application.Service
{
    public class UserService : IUserService
    {
        #region fields
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IUserRepository _userRepository;
        #endregion

        #region ctor
        public UserService(IMongoDatabase mongoDatabase, IUserRepository userRepository)
        {
            this._mongoDatabase = mongoDatabase;
            this._userRepository = userRepository;
        }
        #endregion

        #region methods
        public async Task AddUser(UserDto userDto)
        {
            if (userDto is null)
                throw new Exception();

            var userValidator = new UserValidator(_mongoDatabase);
            var user = await User.Create(Guid.NewGuid(), userDto.Name, userDto.Family, userDto.Sex, userDto.IsMarrid, userDto.IsActive, userDto.Mobile, userDto.Email, userDto.BirthDate, userValidator);

            await this._userRepository.Create(user);
        }

        public async Task<UserDto> Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception();

            var user = await _userRepository.Get(id);
            return user.Map();
        }
        #endregion
    }
}
