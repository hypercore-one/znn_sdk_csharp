using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class OrchestratorInfo : IJsonConvertible<JOrchestratorInfo>
    {
        public OrchestratorInfo(JOrchestratorInfo json)
        {
            WindowSize = json.windowSize;
            KeyGenThreshold = json.keyGenThreshold;
            ConfirmationsToFinality = json.confirmationsToFinality;
            EstimatedMomentumTime = json.estimatedMomentumTime;
            AllowKeyGenHeight = json.allowKeyGenHeight;
        }

        public long WindowSize { get; }
        public long KeyGenThreshold { get; }
        public long ConfirmationsToFinality { get; }
        public long EstimatedMomentumTime { get; }
        public long AllowKeyGenHeight { get; }

        public virtual JOrchestratorInfo ToJson()
        {
            return new JOrchestratorInfo()
            {
                windowSize = WindowSize,
                keyGenThreshold = KeyGenThreshold,
                confirmationsToFinality = ConfirmationsToFinality,
                estimatedMomentumTime = EstimatedMomentumTime,
                allowKeyGenHeight = AllowKeyGenHeight
            };
        }
    }
}
