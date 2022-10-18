using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ShlinkDotnet.Extensions;
using ShlinkDotnet.Models.Create;
using ShlinkDotnet.Web;

var host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        })
        .ConfigureServices((hostContext, services) =>
        {
            services
                .AddOptions()
                .AddHttpClient()
                .AddShlink(hostContext.Configuration.GetSection("shlink"));
            
        })
        .Build();

var apiClient = host.Services.GetRequiredService<ShlinkApiClient>();
await foreach (var it in apiClient.EnumerateShortUrls())
{
    Console.WriteLine($"{it.ShortCode}: {it.LongUrl}");
}

var res = await apiClient.CreateOrUpdateAsync(new CreateShortUrlWithSlugRequest
{
    LongUrl = "https://www.google.at",
    ShortCode = "google"
});

Console.WriteLine("Press any key...");
Console.ReadLine();


