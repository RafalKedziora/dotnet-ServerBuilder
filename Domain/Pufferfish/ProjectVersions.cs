using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pufferfish
{
    public class ProjectVersions
    {
        public string ProjectName { get; set; }
        public ICollection<string> RequestVersionGroups { get; set; }
        public ICollection<string> VersionGroups { get; set; }
        public ICollection<string> Versions { get; set; }
    }
}
