using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class BridgeNetworkInfo : IJsonConvertible<JBridgeNetworkInfo>
    {
        public BridgeNetworkInfo(JBridgeNetworkInfo json)
        {
            NetworkClass = json.networkClass;
            Id = json.id;
            Name = json.name;
            ContractAddress = json.contractAddress;
            Metadata = json.metadata;
            TokenPairs = json.tokenPairs != null
                ? json.tokenPairs.Select(x => new TokenPair(x)).ToArray()
                : new TokenPair[0];
        }

        public int NetworkClass { get; }
        public int Id { get; }
        public string Name { get; }
        public string ContractAddress { get; }
        public string Metadata { get; }
        public TokenPair[] TokenPairs { get; }

        public virtual JBridgeNetworkInfo ToJson()
        {
            return new JBridgeNetworkInfo()
            {
                networkClass = NetworkClass,
                id = Id,
                name = Name,
                contractAddress = ContractAddress,
                metadata = Metadata,
                tokenPairs = TokenPairs.Select(x => x.ToJson()).ToArray(),
            };
        }
    }
}
