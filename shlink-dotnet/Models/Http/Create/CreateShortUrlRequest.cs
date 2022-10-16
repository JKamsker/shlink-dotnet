using Newtonsoft.Json;
using ShlinkDotnet.Models.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShlinkDotnet.Models.Create
{
    public partial class CreateShortUrlWithSlugRequest : BaseShortUrlRequest
    {
        [JsonProperty("customSlug")]
        public string ShortCode { get; set; }
        
        [JsonProperty("domain")]
        public string Domain { get; set; }
    }



    public partial class CreateShorUrlWithRandomCode : BaseShortUrlRequest
    {
        /// <summary>
        /// Can be null if CustomSlug is not null
        /// </summary>
        [JsonProperty("findIfExists", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FindIfExists { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("shortCodeLength", NullValueHandling = NullValueHandling.Ignore)]
        public long? ShortCodeLength { get; set; }
    }
}
