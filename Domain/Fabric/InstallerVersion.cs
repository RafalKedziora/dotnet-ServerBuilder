using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Fabric
{
    public class InstallerVersion
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("stable")]
        public bool Stable { get; set; }
    }
}
