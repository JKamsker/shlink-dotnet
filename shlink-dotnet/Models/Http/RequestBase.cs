using Newtonsoft.Json;

namespace ShlinkDotnet.Models.Http
{
    public class RequestBase
    {
        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties { get; set; }
    }
}
