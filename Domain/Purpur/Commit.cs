using Newtonsoft.Json;

namespace Domain.Purpur
{
    public class Commit
    {
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
