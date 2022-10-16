using System.Diagnostics;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

using ShlinkDotnet.Models.Configuration;
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
                .Configure<ShlinkConfig>(hostContext.Configuration.GetSection("shlink"))
                ;
            
            services.AddTransient<ShlinkRestClient>(x =>
            {
                var config = x.GetRequiredService<IOptions<ShlinkConfig>>().Value;
                var shlinkClient = x.GetRequiredService<IHttpClientFactory>().CreateClient("shlink");
                
                var options = new RestClientOptions(config.BaseUrl)
                {
                    MaxTimeout = 1000,
                };
                
                var client = new RestClient(shlinkClient, options)
                    .UseNewtonsoftJson()
                    .AddDefaultHeader("X-Api-Key", config.ApiKey)
                    ;

                return new ShlinkRestClient(client);
            })
            .AddTransient<ShlinkApiClient>();

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


