
using Movie_Store_Web_Api;
using Movie_Store_Web_Api.DBOperations;
namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            await DataGenerator.Initialize(services);
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}