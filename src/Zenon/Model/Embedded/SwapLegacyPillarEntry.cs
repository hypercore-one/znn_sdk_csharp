using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class SwapLegacyPillarEntry
    {
        public SwapLegacyPillarEntry(Json.JSwapLegacyPillarEntry json)
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

        public virtual Json.JSwapLegacyPillarEntry ToJson()
        {
            return new Json.JSwapLegacyPillarEntry()
            {
                numPillars = NumPillars,
                keyIdHash = KeyIdHash.ToString(),
            };
        }
    }
}
