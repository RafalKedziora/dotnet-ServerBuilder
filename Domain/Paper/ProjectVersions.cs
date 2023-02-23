using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Paper
{
    public class ProjectVersions
    {
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }
        [JsonProperty("project_name")]
        public string ProjectName { get; set; }
        [JsonProperty("version_groups")]
        public ICollection<string> VersionGroups { get; set; }
        [JsonProperty("versions")]
        public ICollection<string> Versions { get; set; }
    }
}
