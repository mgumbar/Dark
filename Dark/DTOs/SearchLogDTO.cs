using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dark.DTOs
{
    public class SearchLogDTO
    {
        [JsonProperty("application")]
        public string Application { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("logName")]
        public string LogName { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
