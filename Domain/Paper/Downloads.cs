using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Paper
{
    public class Downloads
    {
        [JsonProperty("application")]
        public Application Application { get; set; }
        [JsonProperty("mojang_mappings")]
        public MojangMappings MojangMappings { get; set; }
    }
}
