using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Paper
{
    public class Change
    {
        [JsonProperty("commit")]
        public string Commit { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
