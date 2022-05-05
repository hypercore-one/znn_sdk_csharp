using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class SwapApi
    {
        public SwapApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<SwapAssetEntry> GetAssetsByKeyIdHash(string keyIdHash)
        {
            var response = await Client.SendRequest<JSwapAssetEntry>("embedded.swap.getAssetsByKeyIdHash", keyIdHash);
            return new SwapAssetEntry(Hash.Parse(keyIdHash), response);
        }

        public async Task<SwapAssetEntry[]> GetAssets()
        {
            var response = await Client.SendRequest<JObject>("embedded.swap.getAssets");
            var result = new List<SwapAssetEntry>();
            foreach (var token in response)
            {
                result.Add(new SwapAssetEntry(Hash.Parse(token.Key), token.Value.ToObject<JSwapAssetEntry>()));
            }
            return result.ToArray();
        }

        public async Task<SwapLegacyPillarEntry[]> GetLegacyPillars()
        {
            var response = await Client.SendRequest<JSwapLegacyPillarEntry[]>("embedded.swap.getLegacyPillars");
            return response == null ? null : response.Select(x => new SwapLegacyPillarEntry(x)).ToArray();
        }
    }
}
