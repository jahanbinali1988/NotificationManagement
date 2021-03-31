using System;
using Common.Core.Authentications;
using Pawny.Common.Enums;

namespace Common.Core
{
    public interface ICurrentUser
    {
        CurrentUserClaims Get();

        Guid UserId { get; }

        string DeviceId { get; }

        ClientPlatform ClientPlatform  { get; }

        bool IsAuthorized { get; }
    }
}