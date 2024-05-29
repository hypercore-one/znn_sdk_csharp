using System.Numerics;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class PtlcApi
    {
        public PtlcApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<PtlcInfo> GetById(Hash id)
        {
            var response = await Client.SendRequestAsync<JPtlcInfo>("embedded.ptlc.getById", id.ToString());
            return new PtlcInfo(response);
        }

        // Contract methods
        public AccountBlockTemplate Create(TokenStandard tokenStandard, BigInteger amount, long expirationTime, int pointType, byte[] pointLock)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PtlcAddress, tokenStandard, amount,
                Definitions.Ptlc.EncodeFunction("Create", expirationTime, pointType, pointLock));
        }

        public AccountBlockTemplate Reclaim(Hash id)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PtlcAddress, TokenStandard.ZnnZts, 0,
                Definitions.Ptlc.EncodeFunction("Reclaim", id.Bytes));
        }

        public AccountBlockTemplate Unlock(Hash id, byte[] signature)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PtlcAddress, TokenStandard.ZnnZts, 0,
                Definitions.Ptlc.EncodeFunction("Unlock", id.Bytes, signature));
        }

        public AccountBlockTemplate ProxyUnlock(Hash id, Address address, byte[] signature)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PtlcAddress, TokenStandard.ZnnZts, 0,
                Definitions.Ptlc.EncodeFunction("ProxyUnlock", id.Bytes, address, signature));
        }
    }
}
