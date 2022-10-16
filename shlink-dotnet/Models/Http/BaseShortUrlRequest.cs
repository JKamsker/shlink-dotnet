using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ShlinkDotnet.Models.Http
{
    public abstract partial class BaseShortUrlRequest : RequestBase
    {
        [JsonProperty("longUrl")]
        public string LongUrl { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("validSince", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ValidSince { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("validUntil", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ValidUntil { get; set; }

        [JsonProperty("maxVisits", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxVisits { get; set; }

        [JsonProperty("validateUrl", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ValidateUrl { get; set; }

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Tags { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("crawlable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Crawlable { get; set; }

        [JsonProperty("forwardQuery")]
        public bool ForwardQuery { get; set; } = true;

    }
}
