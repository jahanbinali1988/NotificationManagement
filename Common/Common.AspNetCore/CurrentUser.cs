using System;
using System.Linq;
using Common.Authentication;
using Common.Core;
using Common.Core.Authentications;
using Microsoft.AspNetCore.Http;
using Pawny.Common.Enums;

namespace Common.AspNetCore
{
    public class CurrentUser : ICurrentUser
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUserClaims Get()
        {
            var response = new CurrentUserClaims()
            {
                Device = new DeviceInfo()
                {
                    Model = GetClaimValue(GatewayUserClaimsType.DeviceModel),
                    Id = GetClaimValue(GatewayUserClaimsType.DeviceId),
                    Brand = GetClaimValue(GatewayUserClaimsType.DeviceBrand),
                    Platform = ClientPlatform.None
                },
                UserId = GetClaimValue(GatewayUserClaimsType.UserId),
                UserIp = GetClaimValue(GatewayUserClaimsType.UserIp),
                Avatar = GetClaimValue(GatewayUserClaimsType.Avatar),
                Name = GetClaimValue(GatewayUserClaimsType.Name),
                UserAgent = GetClaimValue(GatewayUserClaimsType.UserAgent),
                IsAuthorized = !string.IsNullOrWhiteSpace(GetClaimValue(GatewayUserClaimsType.UserId))
            };

            if (Enum.TryParse(typeof(ClientPlatform), GetClaimValue(GatewayUserClaimsType.Platform), out var platform) && platform != null)
                response.Device.Platform = (ClientPlatform)platform;

            return response;
        }

        public Guid UserId => Guid.Parse(GetClaimValue(GatewayUserClaimsType.UserId));
        public bool IsAuthorized => !string.IsNullOrWhiteSpace(GetClaimValue(GatewayUserClaimsType.UserId));
        public string DeviceId => GetClaimValue(GatewayUserClaimsType.DeviceId);

        public ClientPlatform ClientPlatform
        {
            get
            {
                if (Enum.TryParse(typeof(ClientPlatform), GetClaimValue(GatewayUserClaimsType.Platform), out var platform) && platform != null)
                    return (ClientPlatform)platform;

                return ClientPlatform.None;
            }
        }


        private string GetClaimValue(string claimType)
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(claimType))?.Value;
        }
    }
}