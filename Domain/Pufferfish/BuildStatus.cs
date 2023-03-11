using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pufferfish
{
    public class BuildStatus
    {
        [JsonProperty("artifacts")]
        public List<Artifact> Artifacts { get; set; }
    }
}
