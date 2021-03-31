using System.Linq;
using System.Net;
using System.Net.Http;
using Grpc.Core;

namespace NotificationManagement.Presentation.Api.GRPC.SDK
{
    public static class StatusManager
    {
        public static StatusCode? GetStatusCode(HttpResponseMessage response)
        {
            var headers = response.Headers;

            if (!headers.Contains("grpc-status") && response.StatusCode == HttpStatusCode.OK)
                return StatusCode.OK;

            if (headers.Contains("grpc-status"))
                return (StatusCode)int.Parse(headers.GetValues("grpc-status").First());

            return null;
        }
    }
}
