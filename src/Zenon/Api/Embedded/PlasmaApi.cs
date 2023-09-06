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
    public class PlasmaApi
    {
        public PlasmaApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<PlasmaInfo> Get(Address address)
        {
            var response = await Client.SendRequestAsync<JPlasmaInfo>("embedded.plasma.get", address.ToString());
            return new PlasmaInfo(response);
        }

        public async Task<FusionEntryList> GetEntriesByAddress(Address address, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JFusionEntryList>("embedded.plasma.getEntriesByAddress", address.ToString(), pageIndex, pageSize);
            return new FusionEntryList(response);
        }

        [Obsolete("See issue https://github.com/zenon-network/znn_sdk_dart/issues/5", true)]
        public async Task<long> GetRequiredFusionAmount(long requiredPlasma)
        {
            return await Client.SendRequestAsync<long>("embedded.plasma.getRequiredFusionAmount", requiredPlasma);
        }

        public async Task<BigInteger> GetPlasmaByQsr(BigInteger qsrAmount)
        {
            return await Task.Run(() =>
            {
                return qsrAmount * 2100;
            });
        }

        public async Task<GetRequiredResponse> GetRequiredPoWForAccountBlock(GetRequiredParam powParam)
        {
            var response = await Client.SendRequestAsync<JGetRequiredResponse>("embedded.plasma.getRequiredPoWForAccountBlock", powParam.ToJson());
            return new GetRequiredResponse(response);
        }

        // Contract methods
        public AccountBlockTemplate Fuse(Address beneficiary, BigInteger amount)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PlasmaAddress, TokenStandard.QsrZts, amount,
                Definitions.Plasma.EncodeFunction("Fuse", beneficiary));
        }

        public AccountBlockTemplate Cancel(Hash id)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.PlasmaAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Plasma.EncodeFunction("CancelFuse", id.Bytes));
        }
    }
}
