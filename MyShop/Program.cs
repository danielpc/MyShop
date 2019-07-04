using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API
{
    public class Program
    {
//        public static void Main(string[] args)
//        {
//            var host = BuildWebHost(args);
// 
//            using(var scope = host.Services.CreateScope())
//            using(var context = scope.ServiceProvider.GetService<AppDbContext>())
//            {
//                context.Database.EnsureCreated();
//            }
//
//            host.Run();
//        }
//
//        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//            WebHost.CreateDefaultBuilder(args)
//                .UseStartup<Startup>();
//
//        public static IWebHost BuildWebHost(string[] args) =>
//            WebHost.CreateDefaultBuilder(args)
//                .UseStartup<Startup>()
//                .Build();

        public static void Main(string[] args)
        {
            // throttle the thread pool (set available threads to amount of processors)
            ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

            var host = CreateWebHostBuilder(args).Build();

            // migrate the database.  Best practice = in Main, using service scope
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<AppDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            // run the web app
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                    WebHost.CreateDefaultBuilder(args)
                        .UseStartup<Startup>();
    }
}