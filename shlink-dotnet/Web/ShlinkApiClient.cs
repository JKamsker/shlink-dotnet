using Newtonsoft.Json;

using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

using ShlinkDotnet.Exceptions;
using ShlinkDotnet.Extensions;
using ShlinkDotnet.Models.Create;
using ShlinkDotnet.Models.Error;
using ShlinkDotnet.Models.Http;
using ShlinkDotnet.Models.Update;
using ShlinkDotnet.Web.Http;
using ShlinkDotnet.Web.Http.Constants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace ShlinkDotnet.Web
{


    public class ShlinkRestClient
    {
        private readonly RestClient _restClient;

        public ShlinkRestClient(RestClient restClient)
        {
            _restClient = restClient;
        }

        public static ShlinkRestClient Create(string baseUrl, string apiKey)
        {
            var options = new RestClientOptions(baseUrl)
            {
                //ThrowOnAnyError = true,
                MaxTimeout = 1000,
                
            };
            var client = new RestClient(options)
                .UseNewtonsoftJson()
                .AddDefaultHeader("X-Api-Key", apiKey)
                ;

            return new ShlinkRestClient(client);
        }

        public async Task<ShortUrlQueryResult> FindShortLinksAsync
        (
            string? searchTerm = null,
            IEnumerable<string>? tags = null,
            TagsMode tagsMode = TagsMode.Any,
            OrderBy orderBy = OrderBy.DateCreatedDESC,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null,
            int page = 1,
            int itemsPerPage = 100,
            CancellationToken cancellationToken = default
        )
        {
            var request = new RestRequest("rest/v3/short-urls", Method.Get);
            request.AddHeader("Accept", "application/json");

            request.AddQueryParameter("page", page);
            request.AddQueryParameter("itemsPerPage", itemsPerPage);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                request.AddQueryParameter("searchTerm", searchTerm);
            }

            foreach (var tag in tags ?? Enumerable.Empty<string>())
            {
                request.AddQueryParameter("tags[]", itemsPerPage);
            }

            request
                .AddQueryParameter("tagsMode", tagsMode.ConvertToString())
                .AddQueryParameter("orderBy", orderBy.ConvertToString());

            if (startDate != null)
            {
                request.AddQueryParameter("startDate", startDate?.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            }

            if (endDate != null)
            {
                request.AddQueryParameter("endDate", endDate?.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            }


            var res = await _restClient.ExecuteAsync<ShortUrlQueryResult>(request);
            return res.Data;
        }

        public async Task<ShortUrlDto> CreateShortLinkAsync(BaseShortUrlRequest request, CancellationToken cancellationToken = default)
        {
            var restRequest = new RestRequest("rest/v3/short-urls", Method.Post)
                .AddJsonBody(request);

            var res = await _restClient.ExecuteAsync<ShortUrlDto>(restRequest);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Data;
            }

            var content = JsonConvert.DeserializeObject<BadRequestDto>(res.Content);
            var headers = res.Headers.ToDictionary(h => h.Name, h => h.Value.ToString());
            throw new ApiException<BadRequestDto>("Failed to create short link", res.StatusCode, res.StatusDescription, headers, content, null);
        }

        public async Task<ShortUrlDto> UpdateShortLinkAsync(string shortCode, BaseShortUrlRequest request, CancellationToken cancellationToken = default)
        {
            var restRequest = new RestRequest($"rest/v3/short-urls/{shortCode}", Method.Patch)
                .AddJsonBody(request);

            var res = await _restClient.ExecuteAsync<ShortUrlDto>(restRequest);
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return res.Data;
            }

            var content = JsonConvert.DeserializeObject<BadRequestDto>(res.Content);
            var headers = res.Headers.ToDictionary(h => h.Name, h => h.Value.ToString());
            throw new ApiException<BadRequestDto>($"Failed to update short link: {content.Detail}", res.StatusCode, res.StatusDescription, headers, content, null);
        }
    }

    public class ShlinkApiClient
    {
        private readonly ShlinkRestClient _shlinkRestClient;

        public ShlinkApiClient(ShlinkRestClient shlinkRestClient)
        {
            _shlinkRestClient = shlinkRestClient;
        }

        public async IAsyncEnumerable<ShortUrlDto> EnumerateShortUrls
        (
            string? searchTerm = null,
            IEnumerable<string>? tags = null,
            TagsMode tagsMode = TagsMode.Any,
            OrderBy orderBy = OrderBy.DateCreatedDESC,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null,
            CancellationToken cancellationToken = default
        )
        {
            var page = 1;
            var itemsPerPage = 100;
            var shortUrls = await _shlinkRestClient.FindShortLinksAsync(searchTerm, tags, tagsMode, orderBy, startDate, endDate, page, itemsPerPage, cancellationToken);
            while (shortUrls.ShortUrls.ShortLinks.Length > 0)
            {
                foreach (var shortUrl in shortUrls.ShortUrls.ShortLinks)
                {
                    yield return shortUrl;
                }

                page++;
                if (shortUrls.ShortUrls.Pagination.CurrentPage == shortUrls.ShortUrls.Pagination.PagesCount)
                {
                    yield break;
                }

                shortUrls = await _shlinkRestClient.FindShortLinksAsync(searchTerm, tags, tagsMode, orderBy, startDate, endDate, page, itemsPerPage, cancellationToken);
            }
        }

        public async Task<ShortUrlDto> CreateOrUpdateAsync(CreateShortUrlWithSlugRequest shortUrl)
        {
            try
            {
                var createResult = await _shlinkRestClient.CreateShortLinkAsync(shortUrl);
                return createResult;
            }
            catch (ApiException<BadRequestDto> ex) when (ex.StatusCode == HttpStatusCode.BadRequest && ex.Result?.Type == ErrorType.NonUniqueSlug)
            {
                var updateResult = await _shlinkRestClient.UpdateShortLinkAsync(shortUrl.ShortCode, shortUrl);
                return updateResult;
            }

         ;
        }
    }
}


/*Testcode:
 

//var apiClient = new ShlinkApiClient("https://meet.coderdojo.cf", "c266151c-d613-4aa5-993f-9f48df7254e7");

////await foreach(var item in apiClient.EnumerateShortUrls())
////{
////    Console.WriteLine(item.ShortUrl);
////}
//try
//{
//    var rs = await apiClient.CreateShortUrlAsync(new()
//    {
//        LongUrl = "https://www.google.at/aa",
//        CustomSlug = "HaHa",
//        ForwardQuery = true
//    });
//}
//catch (ApiException<BadRequestResponse> ex) when(ex.Result.Type == ErrorType.NonUniqueSlug)
//{

//	throw;
//}


//Debugger.Break();
 
 */