using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class BridgeInfo : IJsonConvertible<JBridgeInfo>
    {
        public BridgeInfo(JBridgeInfo json)
        {
            Administrator = Address.Parse(json.administrator);
            CompressedTssECDSAPubKey = json.compressedTssECDSAPubKey;
            DecompressedTssECDSAPubKey = json.decompressedTssECDSAPubKey;
            AllowKeyGen = json.allowKeyGen;
            Halted = json.halted;
            UnhaltedAt = json.unhaltedAt;
            UnhaltDurationInMomentums = json.unhaltDurationInMomentums;
            TssNonce = json.tssNonce;
            Metadata = json.metadata;
        }

        public Address Administrator { get; }
        public string CompressedTssECDSAPubKey { get; }
        public string DecompressedTssECDSAPubKey { get; }
        public bool AllowKeyGen { get; }
        public bool Halted { get; }
        public long UnhaltedAt { get; }
        public long UnhaltDurationInMomentums { get; }
        public long TssNonce { get; }
        public string Metadata { get; }

        public virtual JBridgeInfo ToJson()
        {
            return new JBridgeInfo()
            {
                administrator = Administrator.ToString(),
                compressedTssECDSAPubKey = CompressedTssECDSAPubKey,
                decompressedTssECDSAPubKey = DecompressedTssECDSAPubKey,
                allowKeyGen = AllowKeyGen,
                halted = Halted,
                unhaltedAt = UnhaltedAt,
                unhaltDurationInMomentums = UnhaltDurationInMomentums,
                tssNonce = TssNonce,
                metadata = Metadata
            };
        }
    }
}
