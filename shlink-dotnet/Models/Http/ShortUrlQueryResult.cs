using Newtonsoft.Json;

using ShlinkDotnet.Web.Http;

namespace ShlinkDotnet.Models.Http
{
    public partial class ShortUrlQueryResult : RequestBase
    {
        [JsonProperty("shortUrls")]
        public ShortUrls ShortUrls { get; set; }
    }

    public partial class ShortUrls : RequestBase
    {
        [JsonProperty("data")]
        public ShortUrlDto[] ShortLinks { get; set; }

        [JsonProperty("pagination")]
        public PaginationDto Pagination { get; set; }
    }

}
