using System;
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
    public class HtlcApi
    {
        public HtlcApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<HtlcInfo> GetById(Hash id)
        {
            var response = await Client.SendRequestAsync<JHtlcInfo>("embedded.htlc.getById", id.ToString());
            return new HtlcInfo(response);
        }

        public async Task<bool> GetProxyUnlockStatus(Address address)
        {
            return await Client.SendRequestAsync<bool>("embedded.htlc.getProxyUnlockStatus", address.ToString());
        }

        // Contract methods
        public AccountBlockTemplate Create(TokenStandard tokenStandard, BigInteger amount, Address hashLocked, long expirationTime, int hashType, int keyMaxSize, byte[] hashLock)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.HtlcAddress, tokenStandard, amount,
                Definitions.Htlc.EncodeFunction("Create", hashLocked, expirationTime, hashType, keyMaxSize, hashLock));
        }

        public AccountBlockTemplate Reclaim(Hash id)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.HtlcAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Htlc.EncodeFunction("Reclaim", id.Bytes));
        }

        public AccountBlockTemplate Unlock(Hash id, byte[] preimage)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.HtlcAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Htlc.EncodeFunction("Unlock", id.Bytes, preimage));
        }

        public AccountBlockTemplate DenyProxyUnlock()
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.HtlcAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Htlc.EncodeFunction("DenyProxyUnlock"));
        }

        public AccountBlockTemplate AllowProxyUnlock()
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.HtlcAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Htlc.EncodeFunction("AllowProxyUnlock"));
        }
    }
}
