using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Paper
{
    public class Build
    {
        [JsonProperty("build")]
        public int BuildNumber { get; set; }
        [JsonProperty("time")]
        public DateTime Time { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("promoted")]
        public bool Promoted { get; set; }
        [JsonProperty("changes")]
        public List<Change> Changes { get; set; }
        [JsonProperty("downloads")]
        public Downloads Downloads { get; set; }
    }
}
