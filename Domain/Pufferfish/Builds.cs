using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pufferfish
{
    public class Builds
    {
        [JsonProperty("builds")]
        public ICollection<BuildInfo> ExistingsBuilds { get; set; }
    }
}
