using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Paper
{
    public class PaperBuilds
    {
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }
        [JsonProperty("project_name")]
        public string ProjectName { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("builds")]
        public List<Build> Builds { get; set; }
    }
}
