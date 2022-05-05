using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class PillarEpochStats
    {
        public PillarEpochStats(JPillarEpochStats json)
        {
            ProducedMomentums = json.producedMomentums;
            ExpectedMomentums = json.expectedMomentums;
        }

        public long ProducedMomentums { get; }
        public long ExpectedMomentums { get; }

        public virtual JPillarEpochStats ToJson()
        {
            return new JPillarEpochStats()
            {
                producedMomentums = ProducedMomentums,
                expectedMomentums = ExpectedMomentums
            };
        }
    }
}
