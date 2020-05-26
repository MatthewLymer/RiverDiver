using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RiverDiver.Web.App
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("RD_PORT") ?? "5000";
            
            return WebHost
                .CreateDefaultBuilder(args)
                .UseUrls($"http://*:{port}")
                .UseStartup<Startup>();
        }
    }
}
