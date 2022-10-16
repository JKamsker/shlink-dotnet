
using Newtonsoft.Json;
using ShlinkDotnet.Models.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShlinkDotnet.Models.Update
{
    public partial class UpdateShortUrlRequest : BaseShortUrlRequest
    {
        [JsonIgnore]
        public string ShortCode { get; set; }
    }
}
