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
            ZnnFundsNeeded = json.znnFundsNeeded;
            QsrFundsNeeded = json.qsrFundsNeeded;
            CreationTimestamp = json.creationTimestamp;
            Status = (AcceleratorProjectStatus)json.status;
            VoteBreakdown = new VoteBreakdown(json.votes);
        }

        public AcceleratorProject(
            Hash id,
            string name,
            string description,
            string url,
            long znnFundsNeeded,
            long qsrFundsNeeded,
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
        public long ZnnFundsNeeded { get; }
        public long QsrFundsNeeded { get; }
        public long CreationTimestamp { get; }
        public AcceleratorProjectStatus Status { get; }
        public VoteBreakdown VoteBreakdown { get; }

        public double ZnnFundsNeededWithDecimals =>
            AmountUtils.AddDecimals(ZnnFundsNeeded, Constants.ZnnDecimals);

        public double QsrFundsNeededWithDecimals =>
              AmountUtils.AddDecimals(QsrFundsNeeded, Constants.QsrDecimals);

        public virtual void ToJson(JAcceleratorProject json)
        {
            json.id = Id.ToString();
            json.name = Name;
            json.description = Description;
            json.url = Url;
            json.znnFundsNeeded = ZnnFundsNeeded;
            json.qsrFundsNeeded = QsrFundsNeeded;
            json.creationTimestamp = CreationTimestamp;
            json.status = (int)Status;
            json.votes = VoteBreakdown.ToJson();
        }
    }
}
