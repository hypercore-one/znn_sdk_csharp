using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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

        public async Task ToMomentums(SubscriptionCallback callback)
        {
            InitHandler();
            var id = await Client.SendRequestAsync<string>("ledger.subscribe", "momentums");
            SetCallback(id, callback);
        }

        public async Task ToAllAccountBlocks(SubscriptionCallback callback)
        {
            InitHandler();
            var id = await Client.SendRequestAsync<string>("ledger.subscribe", "allAccountBlocks");
            SetCallback(id, callback);
        }

        public async Task ToAccountBlocksByAddress(Address address, SubscriptionCallback callback)
        {
            InitHandler();
            var id = await Client.SendRequestAsync<string>("ledger.subscribe", "accountBlocksByAddress", address.ToString());
            SetCallback(id, callback);
        }

        public async Task ToUnreceivedAccountBlocksByAddress(Address address, SubscriptionCallback callback)
        {
            InitHandler();
            var id = await Client.SendRequestAsync<string>("ledger.subscribe", "unreceivedAccountBlocksByAddress", address.ToString());
            SetCallback(id, callback);
        }

        private SubscriptionHandler Subscriptions { get; set; }

        private void InitHandler()
        {
            if (Subscriptions == null)
            {
                Subscriptions = new SubscriptionHandler();
                Client.Subscribe("ledger.subscription", Subscriptions.HandleGlobalNotification);
            }
        }

        private void SetCallback(string id, SubscriptionCallback callback)
        {
            Subscriptions.SetCallback(id, callback);
        }

        private class SubscriptionHandler
        {
            private Dictionary<string, SubscriptionCallback> callbacks;

            public SubscriptionHandler()
            {
                this.callbacks = new Dictionary<string, SubscriptionCallback>();
            }

            public void SetCallback(string id, SubscriptionCallback callback)
            {
                this.callbacks[id] = callback;
            }

            public void HandleGlobalNotification(string subscription, JToken[] result)
            {
                string id = subscription;
                if (this.callbacks.ContainsKey(id))
                {
                    var callback = this.callbacks[id];
                    if (callback != null)
                    {
                        callback(result);
                    }
                }
            }
        }
    }
}