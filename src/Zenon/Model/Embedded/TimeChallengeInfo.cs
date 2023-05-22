using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class TimeChallengeInfo : IJsonConvertible<JTimeChallengeInfo>
    {
        public TimeChallengeInfo(JTimeChallengeInfo json)
        {
            MethodName = json.methodName;
            ParamsHash = Hash.Parse(json.paramsHash);
            ChallengeStartHeight = json.challengeStartHeight;
        }

        public string MethodName { get; set; }
        public Hash ParamsHash { get; set; }
        public long ChallengeStartHeight { get; set; }

        public virtual JTimeChallengeInfo ToJson()
        {
            return new JTimeChallengeInfo()
            {
                methodName = MethodName,
                paramsHash = ParamsHash.ToString(),
                challengeStartHeight = ChallengeStartHeight
            };
        }
    }
}
