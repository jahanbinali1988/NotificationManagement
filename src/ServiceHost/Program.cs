using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServiceHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((context, op) =>
                    {
                        op.Limits.MaxConcurrentConnections = 100000;
                        op.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(20);
                        op.Listen(new IPEndPoint(IPAddress.Parse(context.Configuration.GetValue<string>("GrpcUrl:Ip")),
                        context.Configuration.GetValue<int>("GrpcUrl:Port")),
                        option =>
                        {
                            option.Protocols = HttpProtocols.Http2;
                            //option.UseHttps();
                        });
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
