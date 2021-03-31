using System;
using Pawny.Common.Enums;

namespace Common.Core.Authentications
{
    public class DeviceInfo
    {
        protected bool Equals(DeviceInfo other)
        {
            return Id == other.Id && Platform == other.Platform && Brand == other.Brand && Model == other.Model;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DeviceInfo) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, (int) Platform, Brand, Model);
        }

        public static bool operator ==(DeviceInfo left, DeviceInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DeviceInfo left, DeviceInfo right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// The unique identifier of device
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Client platform 
        /// </summary>
        public ClientPlatform Platform { get; set; }

        /// <summary>
        /// The brand of device ex: samsung
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// The model of the device ex: A5
        /// </summary>
        public string Model { get; set; }
    }
}