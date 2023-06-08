using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public abstract class AcceleratorProject
    {
        public AcceleratorProject(JAcceleratorProject json)
        {
            Id = Hash.Parse(json.id);
            Name = json.name;
            Description = json.description;
            Url = json.url;
            ZnnFundsNeeded = AmountUtils.ParseAmount(json.znnFundsNeeded);
            QsrFundsNeeded = AmountUtils.ParseAmount(json.qsrFundsNeeded);
            CreationTimestamp = json.creationTimestamp;
            Status = (AcceleratorProjectStatus)json.status;
            VoteBreakdown = new VoteBreakdown(json.votes);
        }

        public AcceleratorProject(
            Hash id,
            string name,
            string description,
            string url,
            BigInteger znnFundsNeeded,
            BigInteger qsrFundsNeeded,
            long creationTimestamp,
            AcceleratorProjectStatus status,
            VoteBreakdown voteBreakdown)
        {
            Id = id;
            Name = name;
            Description = description;
            Url = url;
            ZnnFundsNeeded = znnFundsNeeded;
            QsrFundsNeeded = qsrFundsNeeded;
            CreationTimestamp = creationTimestamp;
            Status = status;
            VoteBreakdown = voteBreakdown;
        }

        public Hash Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Url { get; }
        public BigInteger ZnnFundsNeeded { get; }
        public BigInteger QsrFundsNeeded { get; }
        public long CreationTimestamp { get; }
        public AcceleratorProjectStatus Status { get; }
        public VoteBreakdown VoteBreakdown { get; }

        public virtual void ToJson(JAcceleratorProject json)
        {
            json.id = Id.ToString();
            json.name = Name;
            json.description = Description;
            json.url = Url;
            json.znnFundsNeeded = ZnnFundsNeeded.ToString();
            json.qsrFundsNeeded = QsrFundsNeeded.ToString();
            json.creationTimestamp = CreationTimestamp;
            json.status = (int)Status;
            json.votes = VoteBreakdown.ToJson();
        }
    }
}
