using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Common.AspNetCore
{
    public static class HttpRequestExtensions
    {
        public static string GetUserAgent(this HttpRequest request)
        {
            return request.Headers["User-Agent"].ToString();
        }

        public static IPAddress GetUserIp(this HttpRequest request)
        {
            if (request.Headers["X-Forwarded-For"].FirstOrDefault() != null
                && IPAddress.TryParse(request.Headers["X-Forwarded-For"].ToString(), out var ip))
            {
                return ip;
            }
            return request.HttpContext.Connection.RemoteIpAddress.MapToIPv4();
        }
    }
}