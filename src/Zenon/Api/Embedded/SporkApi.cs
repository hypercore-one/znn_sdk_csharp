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
        public SporkApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<SporkList> GetAll(uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JSporkList>("embedded.spork.getAll", pageIndex, pageSize);
            return new SporkList(response);
        }

        // Contract methods
        public AccountBlockTemplate CreateSpork(string name, string description)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.SporkAddress, TokenStandard.ZnnZts, 0,
                Definitions.Spork.EncodeFunction("CreateSpork", name, description));
        }

        public AccountBlockTemplate ActivateSpork(Hash id)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.SporkAddress, TokenStandard.ZnnZts, 0,
                Definitions.Spork.EncodeFunction("ActivateSpork", id.Bytes));
        }
    }
}
