using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class SwapAssetEntry : IJsonConvertible<JSwapAssetEntry>
    {
        public SwapAssetEntry(Hash keyIdHash, JSwapAssetEntry json)
        {
            KeyIdHash = keyIdHash;
            Qsr = AmountUtils.ParseAmount(json.qsr);
            Znn = AmountUtils.ParseAmount(json.znn);
        }

        public SwapAssetEntry(Hash keyIdHash, BigInteger qsr, BigInteger znn)
        {
            KeyIdHash = keyIdHash;
            Qsr = qsr;
            Znn = znn;
        }

        public Hash KeyIdHash { get; }
        public BigInteger Qsr { get; }
        public BigInteger Znn { get; }

        public virtual JSwapAssetEntry ToJson()
        {
            return new JSwapAssetEntry()
            {
                keyIdHash = KeyIdHash.ToString(),
                qsr = Qsr.ToString(),
                znn = Znn.ToString()
            };
        }

        public bool HasBalance => Qsr > BigInteger.Zero || Znn > BigInteger.Zero;
    }
}
