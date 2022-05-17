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
    public class PlasmaApi
    {
        public PlasmaApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<PlasmaInfo> Get(Address address)
        {
            var response = await Client.SendRequest<JPlasmaInfo>("embedded.plasma.get", address.ToString());
            return new PlasmaInfo(response);
        }

        public async Task<FusionEntryList> GetEntriesByAddress(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JFusionEntryList>("embedded.plasma.getEntriesByAddress", address.ToString(), pageIndex, pageSize);
            return new FusionEntryList(response);
        }

        [Obsolete("See issue https://github.com/zenon-network/znn_sdk_dart/issues/5", true)]
        public async Task<long> GetRequiredFusionAmount(long requiredPlasma)
        {
            return await Client.SendRequest<long>("embedded.plasma.getRequiredFusionAmount", requiredPlasma);
        }

        public async Task<long> GetPlasmaByQsr(double qsrAmount)
        {
            return Convert.ToInt64(qsrAmount * 2100);
        }

        public async Task<GetRequiredResponse> GetRequiredPoWForAccountBlock(GetRequiredParam powParam) 
        {
            var response = await Client.SendRequest<JGetRequiredResponse>("embedded.plasma.getRequiredPoWForAccountBlock", powParam.ToJson());
            return new GetRequiredResponse(response);
        }

        // Contract methods
        public AccountBlockTemplate Fuse(Address beneficiary, int amount)
        {
            return AccountBlockTemplate.CallContract(Address.PlasmaAddress, TokenStandard.QsrZts, amount,
                Definitions.Plasma.EncodeFunction("Fuse", beneficiary));
        }

        public AccountBlockTemplate Cancel(Hash id)
        {
            return AccountBlockTemplate.CallContract(Address.PlasmaAddress, TokenStandard.ZnnZts, 0,
                Definitions.Plasma.EncodeFunction("CancelFuse", id.Bytes));
        }
    }
}
