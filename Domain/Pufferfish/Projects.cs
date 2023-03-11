using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pufferfish
{
    public class Projects
    {
        [JsonProperty("jobs")]
        public ICollection<Job> Jobs { get; set; }
    }
}
