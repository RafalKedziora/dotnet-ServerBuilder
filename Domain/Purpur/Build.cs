using Newtonsoft.Json;

namespace Domain.Purpur
{

    public class Build
    {
        [JsonProperty("build")]
        public string BuildNumber { get; set; }
        [JsonProperty("commits")]
        public List<Commit> Commits { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("md5")]
        public string Md5 { get; set; }
        [JsonProperty("project")]
        public string Project { get; set; }
        [JsonProperty("result")]
        public string Result { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
