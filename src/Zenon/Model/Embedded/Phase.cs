using Newtonsoft.Json.Linq;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class Phase : AcceleratorProject, IJsonConvertible<JPhase>
    {
        public Phase(JPhase json)
            : base(json)
        {
            ProjectId = Hash.Parse(json.projectId);
            AcceptedTimestamp = json.acceptedTimestamp;
        }

        public Phase(
            Hash id,
            Hash projectId,
            string name,
            string description,
            string url,
            long znnFundsNeeded,
            long qsrFundsNeeded,
            long creationTimestamp,
            long acceptedTimestamp,
            AcceleratorProjectStatus status,
            VoteBreakdown voteBreakdown)
            : base(id, name, description, url, znnFundsNeeded, qsrFundsNeeded, creationTimestamp, status, voteBreakdown)
        {
            ProjectId = projectId;
            AcceptedTimestamp = acceptedTimestamp;
        }

        public Hash ProjectId { get; }
        public long AcceptedTimestamp { get; }

        public virtual Json.JPhase ToJson()
        {
            var json = new Json.JPhase();
            base.ToJson(json);
            json.projectId = ProjectId.ToString();
            json.acceptedTimestamp = AcceptedTimestamp;
            return json;
        }
    }
}
