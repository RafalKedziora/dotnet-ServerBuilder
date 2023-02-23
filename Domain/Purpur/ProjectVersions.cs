using Newtonsoft.Json;

namespace Domain.Purpur
{
    public class ProjectVersions
    {
        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("version_groups")]
        public ICollection<string> VersionGroups { get; set; }

        [JsonProperty("versions")]
        public ICollection<string> Versions { get; set; }
    }
}
