using System;

namespace Common.Core.Authentications
{
    public class AccessTokenClaims
    {
        protected bool Equals(AccessTokenClaims other)
        {
            return IsAuthorized == other.IsAuthorized && ClaimUserIp == other.ClaimUserIp &&
                   UserId == other.UserId && Equals(Device, other.Device) &&
                   UserAgent == other.UserAgent && UserIp == other.UserIp && Avatar == other.Avatar && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AccessTokenClaims) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsAuthorized, ClaimUserIp, UserId, Device, UserAgent, UserIp,Avatar,Name);
        }

        public static bool operator ==(AccessTokenClaims left, AccessTokenClaims right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AccessTokenClaims left, AccessTokenClaims right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///True if given access token is valid otherwise false 
        /// </summary>
        public bool IsAuthorized { get; set; }

        /// <summary>
        ///The user public ip address ex:192.168.1.1 which user had at creating access token 
        /// </summary>
        public string ClaimUserIp { get; set; }

        /// <summary>
        /// The unique identifier of the user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The user device meta data
        /// </summary>
        public DeviceInfo Device { get; set; }

        public string UserAgent { get; set; }

        /// <summary>
        /// The user Ip address 
        /// </summary>
        public string UserIp { get; set; }
        /// <summary>
        /// The user avatar
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// The User Name
        /// </summary>
        public string Name { get; set; }

    }
}