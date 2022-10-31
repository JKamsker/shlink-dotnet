using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

using ShlinkDotnet.Models.Configuration;
using ShlinkDotnet.Web;

using System.Net.Http;

namespace ShlinkDotnet.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddShlink(this IServiceCollection services, string baseUrl, string apiKey)
        {
            return services
                .AddShlinkCore()
                .Configure<ShlinkConfig>(cnf =>
                {
                    cnf.ApiKey = apiKey;
                    cnf.BaseUrl = baseUrl;
                });
        }
        
        public static IServiceCollection AddShlink(this IServiceCollection services, IConfigurationSection configuration)
        {
            return services
                .AddShlinkCore()
                .Configure<ShlinkConfig>(configuration);
        }

        private static IServiceCollection AddShlinkCore(this IServiceCollection services)
        {
            return services
                .AddHttpClient()
                .AddTransient<ShlinkRestClient>(x =>
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
        }
    }
}
