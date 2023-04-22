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
    public class SporkApi
    {
        public SporkApi(Lazy<IClient> client)
        {
            Client = client;
        }

        public Lazy<IClient> Client { get; }

        public async Task<SporkList> GetAll(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JSporkList>("embedded.spork.getAll", pageIndex, pageSize);
            return new SporkList(response);
        }

        // Contract methods
        public AccountBlockTemplate CreateSpork(string name, string description)
        {
            return AccountBlockTemplate.CallContract(Address.SporkAddress, TokenStandard.ZnnZts, 0,
                Definitions.Spork.EncodeFunction("CreateSpork", name, description));
        }

        public AccountBlockTemplate ActivateSpork(Hash id)
        {
            return AccountBlockTemplate.CallContract(Address.SporkAddress, TokenStandard.ZnnZts, 0,
                Definitions.Spork.EncodeFunction("ActivateSpork", id.Bytes));
        }
    }
}
