using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace NotificationManagement.Presentation.Api.GRPC.SDK
{
    public static class ServiceExtensions
    {
        public static IServiceCollection NotificationManagementServiceSdk(this IServiceCollection services, int grpcRetryCount, string grpcNotification)
        {
            // Http errors
            var serverErrors = new HttpStatusCode[] {
                HttpStatusCode.BadGateway,
                HttpStatusCode.GatewayTimeout,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.InternalServerError,
                HttpStatusCode.TooManyRequests,
                HttpStatusCode.RequestTimeout
            };

            var gRpcErrors = new StatusCode[] {
                 StatusCode.DeadlineExceeded,
                 StatusCode.Internal,
                 StatusCode.ResourceExhausted,
                 StatusCode.Unavailable,
                 StatusCode.Unknown


            };


            IAsyncPolicy<HttpResponseMessage> RetryFunc(HttpRequestMessage request, ILogger<GRPC.Proto.NotoficationManagementService.NotoficationManagementServiceClient> logger)
            {
                return Policy.HandleResult<HttpResponseMessage>(r =>
                {
                    var grpcStatus = StatusManager.GetStatusCode(r);

                    var httpStatusCode = r.StatusCode;

                    return (grpcStatus == null && serverErrors.Contains(httpStatusCode)) || // if the server send an error before gRPC pipeline
                           (httpStatusCode == HttpStatusCode.OK && grpcStatus.HasValue && gRpcErrors.Contains(grpcStatus.Value)); // if gRPC pipeline handled the request (gRPC always answers OK)
                })
                    .WaitAndRetryAsync(grpcRetryCount,
                        (input) => TimeSpan.FromSeconds(1 + input), (result, timeSpan, retryCount, context) =>
                        {
                            var grpcStatus = StatusManager.GetStatusCode(result.Result);
                            logger.LogCritical($"Request failed with {grpcStatus}. Retry");

                        });
            }

            services.AddGrpcClient<GRPC.Proto.NotoficationManagementService.NotoficationManagementServiceClient>(options =>
            {
                options.Address = new Uri(grpcNotification);

            }).AddPolicyHandler(
                (provider, message) => RetryFunc(message, provider.GetService<ILogger<GRPC.Proto.NotoficationManagementService.NotoficationManagementServiceClient>>()));


            //todo:Configure tls and then clean this line
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            return services;

        }
    }
}
