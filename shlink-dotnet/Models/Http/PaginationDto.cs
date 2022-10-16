using Newtonsoft.Json;

namespace ShlinkDotnet.Models.Http
{
    public partial class PaginationDto
    {
        [JsonProperty("currentPage")]
        public long CurrentPage { get; set; }

        [JsonProperty("pagesCount")]
        public long PagesCount { get; set; }

        [JsonProperty("itemsPerPage")]
        public long ItemsPerPage { get; set; }

        [JsonProperty("itemsInCurrentPage")]
        public long ItemsInCurrentPage { get; set; }

        [JsonProperty("totalItems")]
        public long TotalItems { get; set; }
    }



}
