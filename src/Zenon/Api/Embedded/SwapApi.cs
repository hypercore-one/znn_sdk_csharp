﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
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

        public async Task<SwapAssetEntry> GetAssetsByKeyIdHash(Hash keyIdHash)
        {
            var response = await Client.SendRequestAsync<JSwapAssetEntry>("embedded.swap.getAssetsByKeyIdHash", keyIdHash.ToString());
            return new SwapAssetEntry(keyIdHash, response);
        }

        public async Task<SwapAssetEntry[]> GetAssets()
        {
            var response = await Client.SendRequestAsync<JObject>("embedded.swap.getAssets");
            var result = new List<SwapAssetEntry>();
            foreach (var token in response)
            {
                result.Add(new SwapAssetEntry(Hash.Parse(token.Key), token.Value.ToObject<JSwapAssetEntry>()));
            }
            return result.ToArray();
        }

        public async Task<SwapLegacyPillarEntry[]> GetLegacyPillars()
        {
            var response = await Client.SendRequestAsync<JSwapLegacyPillarEntry[]>("embedded.swap.getLegacyPillars");
            return response == null ? null : response.Select(x => new SwapLegacyPillarEntry(x)).ToArray();
        }

        // Contract methods
        public AccountBlockTemplate RetrieveAssets(string pubKey, string signature)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.SwapAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Swap.EncodeFunction("RetrieveAssets", pubKey, signature));
        }

        public long GetSwapDecayPercentage(int currentTimestamp)
        {
            const int secondsPerDay = 86400;

            var percentageToGive = 100;
            var currentEpoch =
                (currentTimestamp - Constants.GenesisTimestamp) / secondsPerDay;

            if (currentTimestamp < Constants.SwapAssetDecayTimestampStart)
            {
                percentageToGive = 100;
            }
            else
            {
                var numTicks = (int)((currentEpoch - Constants.SwapAssetDecayEpochsOffset + 1) / Constants.SwapAssetDecayTickEpochs);
                var decayFactor = Constants.SwapAssetDecayTickValuePercentage * numTicks;
                if (decayFactor > 100)
                {
                    percentageToGive = 0;
                }
                else
                {
                    percentageToGive = 100 - decayFactor;
                }
            }
            return 100 - percentageToGive;
        }
    }
}
