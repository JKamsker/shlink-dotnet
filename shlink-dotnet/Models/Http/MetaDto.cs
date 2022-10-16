using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ShlinkDotnet.Models.Http
{
    public partial class MetaDto
    {
        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("validSince")]
        public DateTimeOffset? ValidSince { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("validUntil")]
        public DateTimeOffset? ValidUntil { get; set; }

        [JsonProperty("maxVisits")]
        public long? MaxVisits { get; set; }
    }



}
