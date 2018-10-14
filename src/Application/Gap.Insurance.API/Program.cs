using Gap.Domain.Customer.Persistence;
using Gap.Domain.Insurance.Persistence;
using Gap.Infrastructure.WebHost;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Gap.Insurance.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<CustomerContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<CustomerContextSeed>>();
                    new CustomerContextSeed()
                        .SeedAsync(context, logger)
                        .Wait();
                })
                .MigrateDbContext<InsuranceContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<InsuranceContextSeed>>();
                    new InsuranceContextSeed()
                        .SeedAsync(context, logger)
                        .Wait();
                })
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .Build();
    }
}
