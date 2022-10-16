using Newtonsoft.Json;
using ShlinkDotnet.Models.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShlinkDotnet.Models.Error
{


    public partial class BadRequestDto : RequestBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }
        
        [JsonProperty("shortCode")]
        public string ShortCode { get; set; }

        [JsonProperty("invalidElements")]
        public string[] InvalidElements { get; set; }

    }
}
