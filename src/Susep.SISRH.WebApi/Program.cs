using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Susep.SISRH.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                    config
                        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"))
                        .AddJsonFile($"connectionStrings.{environmentName}.json", true, true)
                        .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                        .AddJsonFile($"messagebroker.{environmentName}.json", true, true)
                        .AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
