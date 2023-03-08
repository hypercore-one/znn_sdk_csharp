using System;
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
        public PtlcApi(Lazy<IClient> client)
        {
            Client = client;
        }

        public Lazy<IClient> Client { get; }

        public async Task<PtlcInfo> GetById(Hash id)
        {
            var response = await Client.Value.SendRequest<JPtlcInfo>("embedded.ptlc.getById", id.ToString());
            return new PtlcInfo(response);
        }

        // Contract methods
        public AccountBlockTemplate Create(TokenStandard tokenStandard, long amount, long expirationTime, int pointType, byte[] pointLock)
        {
            return AccountBlockTemplate.CallContract(Address.PtlcAddress, tokenStandard, amount,
                Definitions.Ptlc.EncodeFunction("Create", expirationTime, pointType, pointLock));
        }

        public AccountBlockTemplate Reclaim(Hash id)
        {
            return AccountBlockTemplate.CallContract(Address.PtlcAddress, TokenStandard.ZnnZts, 0,
                Definitions.Ptlc.EncodeFunction("Reclaim", id.Bytes));
        }

        public AccountBlockTemplate Unlock(Hash id, byte[] signature)
        {
            return AccountBlockTemplate.CallContract(Address.PtlcAddress, TokenStandard.ZnnZts, 0,
                Definitions.Ptlc.EncodeFunction("Unlock", id.Bytes, signature));
        }
    }
}
