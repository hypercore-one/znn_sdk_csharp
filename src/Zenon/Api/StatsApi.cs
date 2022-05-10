using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Json;

namespace Zenon.Api
{
    public class StatsApi
    {
        public StatsApi(IClient client)
        {
            this.Client = client;
        }

        public IClient Client { get; }

        public async Task<JOsInfo> OsInfo()
        {
            return await Client.SendRequest<JOsInfo>("stats.osInfo");
        }

        public async Task<JProcessInfo> ProcessInfo()
        {
            return await Client.SendRequest<JProcessInfo>("stats.processInfo");
        }

        public async Task<JNetworkInfo> NetworkInfo()
        {
            return await Client.SendRequest<JNetworkInfo>("stats.networkInfo");
        }

        public async Task<JSyncInfo> SyncInfo()
        {
            return await Client.SendRequest<JSyncInfo>("stats.syncInfo");
        }
    }
}