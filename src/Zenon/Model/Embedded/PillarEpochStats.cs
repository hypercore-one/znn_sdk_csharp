using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class PillarEpochStats : IJsonConvertible<JPillarEpochStats>
    {
        public PillarEpochStats(JPillarEpochStats json)
        {
            ProducedMomentums = json.producedMomentums;
            ExpectedMomentums = json.expectedMomentums;
        }

        public int ProducedMomentums { get; }
        public int ExpectedMomentums { get; }

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
