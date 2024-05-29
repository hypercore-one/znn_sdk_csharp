using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class WrapTokenRequest : IJsonConvertible<JWrapTokenRequest>
    {
        public WrapTokenRequest(JWrapTokenRequest json)
        {
            NetworkClass = json.networkClass;
            ChainId = json.chainId;
            Id = Hash.Parse(json.id);
            ToAddress = json.toAddress;
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            TokenAddress = json.tokenAddress;
            Amount = AmountUtils.ParseAmount(json.amount);
            Fee = AmountUtils.ParseAmount(json.fee);
            Signature = json.signature;
            CreationMomentumHeight = json.creationMomentumHeight;
        }

        public uint NetworkClass { get; }
        public uint ChainId { get; }
        public Hash Id { get; }
        public string ToAddress { get; }
        public TokenStandard TokenStandard { get; }
        public string TokenAddress { get; }
        public BigInteger Amount { get; }
        public BigInteger Fee { get; }
        public string Signature { get; }
        public ulong CreationMomentumHeight { get; }

        public virtual JWrapTokenRequest ToJson()
        {
            return new JWrapTokenRequest()
            {
                networkClass = NetworkClass,
                chainId = ChainId,
                id = Id.ToString(),
                toAddress = ToAddress,
                tokenStandard = TokenStandard.ToString(),
                tokenAddress = TokenAddress,
                amount = Amount.ToString(),
                fee = Fee.ToString(),
                signature = Signature,
                creationMomentumHeight = CreationMomentumHeight,
            };
        }
    }
}
