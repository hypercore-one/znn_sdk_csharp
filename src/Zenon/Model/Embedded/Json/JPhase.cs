using Newtonsoft.Json.Linq;

namespace Zenon.Model.Embedded.Json
{
    public class JPhase : JAcceleratorProject
    {
        public static JPhase FromJObject(JObject json)
        {
            var phase = new JPhase();
            phase.projectId = json["phase"].Value<string>("projectID");
            phase.acceptedTimestamp = json["phase"].Value<long>("acceptedTimestamp");
            phase.id = json["phase"].Value<string>("id");
            phase.name = json["phase"].Value<string>("name");
            phase.description = json["phase"].Value<string>("description");
            phase.url = json["phase"].Value<string>("url");
            phase.znnFundsNeeded = json["phase"].Value<string>("znnFundsNeeded");
            phase.qsrFundsNeeded = json["phase"].Value<string>("qsrFundsNeeded");
            phase.creationTimestamp = json["phase"].Value<long>("creationTimestamp");
            phase.status = json["phase"].Value<int>("status");
            phase.votes = json["votes"].ToObject<JVoteBreakdown>();
            return phase;
        }

        public static JObject ToJObject(JPhase phase)
        {
            return JObject.FromObject(phase);
        }

        public string projectId { get; set; }
        public long acceptedTimestamp { get; set; }
    }
}
