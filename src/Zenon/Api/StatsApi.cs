using System;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Json;

namespace Zenon.Api
{
    public class StatsApi
    {
        public StatsApi(Lazy<IClient> client)
        {
            this.Client = client;
        }

        public Lazy<IClient> Client { get; }

        public async Task<JOsInfo> OsInfo()
        {
            return await Client.Value.SendRequest<JOsInfo>("stats.osInfo");
        }

        public async Task<JProcessInfo> ProcessInfo()
        {
            return await Client.Value.SendRequest<JProcessInfo>("stats.processInfo");
        }

        public async Task<JNetworkInfo> NetworkInfo()
        {
            return await Client.Value.SendRequest<JNetworkInfo>("stats.networkInfo");
        }

        public async Task<JSyncInfo> SyncInfo()
        {
            return await Client.Value.SendRequest<JSyncInfo>("stats.syncInfo");
        }
    }
}