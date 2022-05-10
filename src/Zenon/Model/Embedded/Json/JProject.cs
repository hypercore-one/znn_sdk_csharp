using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Zenon.Model.Embedded.Json
{
    public class JProject : JAcceleratorProject
    {
        public string owner { get; set; }
        public string[] phaseIds { get; set; }
        public IEnumerable<JObject> phases { get; set; }
        public long lastUpdateTimestamp { get; set; }
    }
}
