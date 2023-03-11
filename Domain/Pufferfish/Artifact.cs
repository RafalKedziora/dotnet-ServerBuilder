using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pufferfish
{
    public class Artifact
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("relativePath")]
        public string DownloadEnginePath { get; set; }
    }
}
