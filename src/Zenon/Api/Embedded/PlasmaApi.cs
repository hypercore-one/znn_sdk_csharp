using System;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
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

        public async Task<long> GetRequiredFusionAmount(long requiredPlasma)
        {
            return await Client.SendRequest<long>("embedded.plasma.getRequiredFusionAmount", requiredPlasma);
        }

        public async Task<long> GetPlasmaByQsr(double qsrAmount)
        {
            return Convert.ToInt64(qsrAmount * 2100);
        }
    }
}
