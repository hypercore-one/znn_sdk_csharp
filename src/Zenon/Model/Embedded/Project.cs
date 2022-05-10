using Newtonsoft.Json.Linq;
using System.Linq;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class Project : AcceleratorProject, IJsonConvertible<JProject>
    {
        public Project(JProject json)
            : base(json)
        {
            Owner = Address.Parse(json.owner);
            LastUpdateTimestamp = json.lastUpdateTimestamp;
            Phases = json.phases.Select(x => new Phase(JPhase.FromJObject(x))).ToArray();
            PhaseIds = json.phaseIds.Select(x => Hash.Parse(x)).ToArray();
        }

        public Project(
            Hash id,
            string name,
            Address owner,
            string description,
            string url,
            long znnFundsNeeded,
            long qsrFundsNeeded,
            long creationTimestamp,
            long lastUpdateTimestamp,
            AcceleratorProjectStatus status,
            Hash[] phaseIds,
            VoteBreakdown voteBreakdown,
            Phase[] phases)
            : base(id, name, description, url, znnFundsNeeded, qsrFundsNeeded, creationTimestamp, status, voteBreakdown)
        {
            Owner = owner;
            PhaseIds = phaseIds;
            Phases = phases;
            LastUpdateTimestamp = lastUpdateTimestamp;
        }

        public Address Owner { get; }
        public Hash[] PhaseIds { get; }
        public Phase[] Phases { get; }
        public long LastUpdateTimestamp { get; }

        public virtual JProject ToJson()
        {
            var json = new JProject();
            base.ToJson(json);
            json.owner = Owner.ToString();
            json.lastUpdateTimestamp = LastUpdateTimestamp;
            json.phases = Phases.Select(x => JPhase.ToJObject(x.ToJson())).ToArray();
            json.phaseIds = PhaseIds.Select(x => x.ToString()).ToArray();
            return json;
        }

        public long GetPaidZnnFunds()
        {
            long amount = 0;
            foreach (var phase in Phases)
            {
                if (phase.Status == AcceleratorProjectStatus.Paid)
                {
                    amount += phase.ZnnFundsNeeded;
                }
            }
            return amount;
        }

        public long GetPendingZnnFunds()
        {
            if (Phases.Length == 0)
                return 0;

            var lastPhase = GetLastPhase();
            if (lastPhase != null &&
                lastPhase.Status == AcceleratorProjectStatus.Active)
            {
                return lastPhase.ZnnFundsNeeded;
            }
            return 0;
        }

        public long GetRemainingZnnFunds()
        {
            if (Phases.Length == 0)
                return ZnnFundsNeeded;

            return ZnnFundsNeeded - GetPaidZnnFunds();
        }

        public long GetTotalZnnFunds()
        {
            return ZnnFundsNeeded;
        }

        public long GetPaidQsrFunds()
        {
            long amount = 0;
            foreach (var phase in Phases)
            {
                if (phase.Status == AcceleratorProjectStatus.Paid)
                {
                    amount += phase.QsrFundsNeeded;
                }
            }
            return amount;
        }

        public long GetPendingQsrFunds()
        {
            if (Phases.Length == 0)
                return 0;

            var lastPhase = GetLastPhase();
            if (lastPhase != null &&
                lastPhase.Status == AcceleratorProjectStatus.Active)
            {
                return lastPhase.QsrFundsNeeded;
            }
            return 0;
        }

        public long GetRemainingQsrFunds()
        {
            if (Phases.Length == 0)
                return QsrFundsNeeded;

            return QsrFundsNeeded - GetPaidQsrFunds();
        }

        public long GetTotalQsrFunds()
        {
            return QsrFundsNeeded;
        }

        public Phase FindPhaseById(Hash id)
        {
            for (var i = 0; i < PhaseIds.Length; i++)
            {
                if (id == PhaseIds[i])
                {
                    return Phases[i];
                }
            }
            return null;
        }

        public Phase GetLastPhase()
        {
            if (Phases.Length == 0)
                return null;

            return Phases.Last();
        }
    }
}
