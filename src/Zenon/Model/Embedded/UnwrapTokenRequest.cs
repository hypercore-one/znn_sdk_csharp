using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class UnwrapTokenRequest : IJsonConvertible<JUnwrapTokenRequest>
    {
        public UnwrapTokenRequest(JUnwrapTokenRequest json)
        {
            RegistrationMomentumHeight = json.registrationMomentumHeight;
            NetworkClass = json.networkClass;
            ChainId = json.chainId;
            TransactionHash = Hash.Parse(json.transactionHash);
            LogIndex = json.logIndex;
            ToAddress = Address.Parse(json.toAddress);
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            TokenAddress = json.tokenAddress;
            Amount = json.amount;
            Signature = json.signature;
            Redeemed = json.redeemed;
            Revoked = json.revoked;
        }

        public long RegistrationMomentumHeight { get; }
        public int NetworkClass { get; }
        public int ChainId { get; }
        public Hash TransactionHash { get; }
        public int LogIndex { get; }
        public Address ToAddress { get; }
        public TokenStandard TokenStandard { get; }
        public string TokenAddress { get; }
        public long Amount { get; }
        public string Signature { get; }
        public int Redeemed { get; }
        public int Revoked { get; }

        public virtual JUnwrapTokenRequest ToJson()
        {
            return new JUnwrapTokenRequest()
            {
                registrationMomentumHeight = RegistrationMomentumHeight,
                networkClass = NetworkClass,
                transactionHash = TransactionHash.ToString(),
                logIndex = LogIndex,
                chainId = ChainId,
                toAddress = ToAddress.ToString(),
                tokenStandard = TokenStandard.ToString(),
                tokenAddress = TokenAddress,
                amount = Amount,
                signature = Signature,
                redeemed = Redeemed,
                revoked = Revoked
            };
        }
    }
}
