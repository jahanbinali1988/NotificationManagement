using NotificationManagement.Domain.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Domain.Tests.Unit.Utility
{
    public class UserTestBuilder
    {
        #region MyRegion
        private Guid _id;
        private string _name;
        private string _family;
        private bool _sex;
        private bool _isMarrid;
        private bool _isActive;
        private string _mobile;
        private string _email;
        private DateTime _birthDate;
        private readonly IUserValidator _userValidator;
        #endregion

        #region ctor
        public UserTestBuilder()
        {
            Random random = new Random();

            this._id = Guid.NewGuid();
            this._name = Guid.NewGuid().ToString();
            this._family = Guid.NewGuid().ToString();
            this._sex = Convert.ToBoolean(random.Next(0, 1));
            this._isMarrid = Convert.ToBoolean(random.Next(0, 1));
            this._isActive = Convert.ToBoolean(random.Next(0, 1));
            this._mobile = "09" + random.Next(100000000, 399999999);
            this._email = Guid.NewGuid() + "@gmail.com";
            this._birthDate = DateTime.Now.AddYears(random.Next(-40, -15));
            this._userValidator = Substitute.For<IUserValidator>();
        }
        #endregion

        #region methods
        public UserTestBuilder WithId(Guid id)
        {
            this._id = id;
            return this;
        }

        public UserTestBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }

        public UserTestBuilder WithFamily(string family)
        {
            this._family = family;
            return this;
        }

        public UserTestBuilder WithSex(bool sex)
        {
            this._sex = sex;
            return this;
        }

        public UserTestBuilder WithIsMarried(bool isMarried)
        {
            this._isMarrid = isMarried;
            return this;
        }

        public UserTestBuilder WithIsActive(bool isActive)
        {
            this._isActive = isActive;
            return this;
        }

        public UserTestBuilder WithMobile(string mobile)
        {
            this._mobile = mobile;
            return this;
        }

        public UserTestBuilder WithEmail(string email)
        {
            this._email = email;
            return this;
        }

        public UserTestBuilder WithBirthDate(DateTime birthdate)
        {
            this._birthDate = birthdate;
            return this;
        }

        public UserTestBuilder IsEmailDuplicate(bool returnValue)
        {
            this._userValidator.IsDuplicateEmail(this._email).Returns(returnValue);
            return this;
        }

        public UserTestBuilder IsMobileDuplicate(bool returnValue)
        {
            this._userValidator.IsDuplicateMobile(this._mobile).Returns(returnValue);
            return this;
        }

        public async Task<Models.User.User> Build()
        {
            return await Models.User.User.Create(this._id, this._name, this._family, this._sex, this._isMarrid, this._isActive, this._mobile, this._email, this._birthDate, this._userValidator);
        }
        #endregion
    }
}
