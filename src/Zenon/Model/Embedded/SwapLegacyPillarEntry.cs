using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class SwapLegacyPillarEntry : IJsonConvertible<JSwapLegacyPillarEntry>
    {
        public SwapLegacyPillarEntry(JSwapLegacyPillarEntry json)
        {
            NumPillars = json.numPillars;
            KeyIdHash = Hash.Parse(json.keyIdHash);
        }

        public SwapLegacyPillarEntry(long numPillars, Hash keyIdHash)
        {
            NumPillars = numPillars;
            KeyIdHash = keyIdHash;
        }

        public long NumPillars { get; }
        public Hash KeyIdHash { get; }

        public virtual JSwapLegacyPillarEntry ToJson()
        {
            return new JSwapLegacyPillarEntry()
            {
                numPillars = NumPillars,
                keyIdHash = KeyIdHash.ToString(),
            };
        }
    }
}
