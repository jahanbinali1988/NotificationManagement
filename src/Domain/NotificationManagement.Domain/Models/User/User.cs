using Common.Domain;
using NotificationManagement.Domain.Exceptions;
using NotificationManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Domain.Models.User
{
    public class User : AggregateRoot
    {
        #region fields
        public string Name { get; private set; }
        public string Family { get; private set; }
        public bool Sex { get; private set; }
        public bool IsMarrid { get; private set; }
        public bool IsActive { get; private set; }
        public string Mobile { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        #endregion

        #region ctor
        private User(Guid id, string name, string family, bool sex, bool isMarried, bool isActive, string mobile, string email, DateTime birthDate)
        {
            this.Id = id;
            this.Name = name;
            this.Family = family;
            this.Sex = sex;
            this.IsMarrid = isMarried;
            this.BirthDate = birthDate;
            this.CreatedAt = DateTimeOffset.Now;
            this.IsActive = isActive;
            this.Email = email;
            this.Mobile = mobile;
        }

        private User()
        {

        }
        #endregion

        #region method
        public static async Task<User> Create(Guid id, string name, string family, bool sex, bool isMarried, bool isActive, string mobile, string email, DateTime birthDate, IUserValidator userValidator)
        {
            var isEmailDuplicate = await userValidator.IsDuplicateEmail(email);
            var isMobileDuplicate = await userValidator.IsDuplicateMobile(mobile);

            GuardAgainstEmailUserDuplicate(isEmailDuplicate);
            GuardAgainstMobileUserDuplicate(isMobileDuplicate);

            return new User(id, name, family, sex, isMarried, isActive, mobile, email, birthDate);
        }

        private static void GuardAgainstEmailUserDuplicate(bool isDuplicate)
        {
            if (isDuplicate)
                throw new UserEmailDuplicateException();
        }

        private static void GuardAgainstMobileUserDuplicate(bool isDuplicate)
        {
            if (isDuplicate)
                throw new UserMobileDuplicateException();
        }
        #endregion

    }
}
