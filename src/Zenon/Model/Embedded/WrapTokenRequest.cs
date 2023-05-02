using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

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
            Amount = json.amount;
            Fee = json.fee;
            Signature = json.signature;
            CreationMomentumHeight = json.creationMomentumHeight;
        }

        public int NetworkClass { get; }
        public int ChainId { get; }
        public Hash Id { get; }
        public string ToAddress { get; }
        public TokenStandard TokenStandard { get; }
        public string TokenAddress { get; }
        public long Amount { get; }
        public long Fee { get; }
        public string Signature { get; }
        public long CreationMomentumHeight { get; }

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
                amount = Amount,
                fee = Fee,
                signature = Signature,
                creationMomentumHeight = CreationMomentumHeight,
            };
        }
    }
}
