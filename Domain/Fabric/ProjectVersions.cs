using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Fabric
{
    public class ProjectVersions
    {
        public ICollection<string> Versions { get; set; }
        public ICollection<string> VersionGroups { get; set; }
    }
}
