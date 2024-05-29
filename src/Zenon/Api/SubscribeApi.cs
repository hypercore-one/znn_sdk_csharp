using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Primitives;

namespace Zenon.Api
{
    public delegate void SubscriptionCallback(JToken[] result);

    public class SubscribeApi
    {
        public SubscribeApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<string> ToMomentums()
        {
            return await Client.SendRequestAsync<string>("ledger.subscribe", "momentums");
        }

        public async Task<string> ToAllAccountBlocks()
        {
            return await Client.SendRequestAsync<string>("ledger.subscribe", "allAccountBlocks");
        }

        public async Task<string> ToAccountBlocksByAddress(Address address)
        {
            return await Client.SendRequestAsync<string>("ledger.subscribe", "accountBlocksByAddress", address.ToString());
        }

        public async Task<string> ToUnreceivedAccountBlocksByAddress(Address address)
        {
            return await Client.SendRequestAsync<string>("ledger.subscribe", "unreceivedAccountBlocksByAddress", address.ToString());
        }
    }
}