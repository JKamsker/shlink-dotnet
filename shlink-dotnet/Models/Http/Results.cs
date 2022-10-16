using Newtonsoft.Json;

using ShlinkDotnet.Web.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShlinkDotnet.Models.Http
{
    public partial class ShortUrlDto
    {
        [JsonProperty("shortCode")]
        public string ShortCode { get; set; }

        [JsonProperty("shortUrl")]
        public string ShortUrl { get; set; }

        [JsonProperty("longUrl")]
        public string LongUrl { get; set; }

        [JsonProperty("dateCreated")]
        public DateTimeOffset? DateCreated { get; set; }

        [JsonProperty("visitsCount")]
        public long VisitsCount { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("meta")]
        public MetaDto Meta { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("crawlable")]
        public bool Crawlable { get; set; }

        [JsonProperty("forwardQuery")]
        public bool ForwardQuery { get; set; }
    }
}
