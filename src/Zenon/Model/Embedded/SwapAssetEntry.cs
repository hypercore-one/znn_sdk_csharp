using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class SwapAssetEntry : IJsonConvertible<JSwapAssetEntry>
    {
        public SwapAssetEntry(Hash keyIdHash, JSwapAssetEntry json)
        {
            KeyIdHash = keyIdHash;
            Qsr = json.qsr;
            Znn = json.znn;
        }

        public SwapAssetEntry(Hash keyIdHash, long qsr, long znn)
        {
            KeyIdHash = keyIdHash;
            Qsr = qsr;
            Znn = znn;
        }

        public Hash KeyIdHash { get; }
        public long Qsr { get; }
        public long Znn { get; }

        public virtual JSwapAssetEntry ToJson()
        {
            return new JSwapAssetEntry()
            {
                keyIdHash = KeyIdHash.ToString(),
                qsr = Qsr,
                znn = Znn
            };
        }

        public bool HasBalance => Qsr > 0 || Znn > 0;
    }
}
