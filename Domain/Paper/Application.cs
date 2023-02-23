using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Paper
{
    public class Application
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sha256")]
        public string Sha256 { get; set; }
    }
}
