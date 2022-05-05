using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Primitives;

namespace Zenon.Api
{
    public class SubscribeApi
    {
        public SubscribeApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<string> ToMomentums()
        {
            return await Client.SendRequest<string>("ledger.subscribe", "momentums");
        }

        public async Task<string> ToAllAccountBlocks()
        {
            return await Client.SendRequest<string>("ledger.subscribe", "allAccountBlocks");
        }

        public async Task<string> ToAccountBlocksByAddress(Address address)
        {
            return await Client.SendRequest<string>("ledger.subscribe", "accountBlocksByAddress", address.ToString());
        }

        public async Task<string> ToUnreceivedAccountBlocksByAddress(Address address)
        {
            return await Client.SendRequest<string>("ledger.subscribe", "unreceivedAccountBlocksByAddress", address.ToString());
        }
    }
}