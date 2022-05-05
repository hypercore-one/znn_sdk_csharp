using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class AccountBlockConfirmationDetail
    {
        public AccountBlockConfirmationDetail(JAccountBlockConfirmationDetail json)
        {
            NumConfirmations = json.numConfirmations;
            MomentumHeight = json.momentumHeight;
            MomentumHash = Hash.Parse(json.momentumHash);
            MomentumTimestamp = json.momentumTimestamp;
        }

        public long NumConfirmations { get; }
        public long MomentumHeight { get; }
        public Hash MomentumHash { get; }
        public long MomentumTimestamp { get; }

        public virtual JAccountBlockConfirmationDetail ToJson()
        {
            return new JAccountBlockConfirmationDetail()
            {
                numConfirmations = NumConfirmations,
                momentumHeight = MomentumHeight,
                momentumHash = MomentumHash.ToString(),
                momentumTimestamp = MomentumTimestamp
            };
        }
    }
}
