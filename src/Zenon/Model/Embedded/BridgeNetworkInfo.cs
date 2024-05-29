using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class BridgeNetworkInfo : IJsonConvertible<JBridgeNetworkInfo>
    {
        public BridgeNetworkInfo(JBridgeNetworkInfo json)
        {
            NetworkClass = json.networkClass;
            ChainId = json.chainId;
            Name = json.name;
            ContractAddress = json.contractAddress;
            Metadata = json.metadata;
            TokenPairs = json.tokenPairs != null
                ? json.tokenPairs.Select(x => new TokenPair(x)).ToArray()
                : new TokenPair[0];
        }

        public uint NetworkClass { get; }
        public uint ChainId { get; }
        public string Name { get; }
        public string ContractAddress { get; }
        public string Metadata { get; }
        public TokenPair[] TokenPairs { get; }

        public virtual JBridgeNetworkInfo ToJson()
        {
            return new JBridgeNetworkInfo()
            {
                networkClass = NetworkClass,
                chainId = ChainId,
                name = Name,
                contractAddress = ContractAddress,
                metadata = Metadata,
                tokenPairs = TokenPairs.Select(x => x.ToJson()).ToArray(),
            };
        }
    }
}
