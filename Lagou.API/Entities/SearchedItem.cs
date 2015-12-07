using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API.Entities {
    public class SearchedItem {

        [JsonProperty("positionId")]
        public int PositionId { get; set; }


        [JsonProperty("positionName")]
        public string PositionName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("createTime")]
        public string CreateTime { get; set; }

        [JsonProperty("salary")]
        public string Salary { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("companyLogo")]
        public string CompanyLogo { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("positionFirstType")]
        public string PositionFirstType { get; set; }
    }
}
