using System;
using Zenon.Api.Embedded;
using Zenon.Client;

namespace Zenon.Api
{
    public class EmbeddedApi
    {
        public EmbeddedApi(Lazy<IClient> client)
        {
            Client = client;

            Pillar = new PillarApi(client);
            Plasma = new PlasmaApi(client);
            Sentinel = new SentinelApi(client);
            Stake = new StakeApi(client);
            Swap = new SwapApi(client);
            Token = new TokenApi(client);
            Accelerator = new AcceleratorApi(client);
        }

        public Lazy<IClient> Client { get; }

        public PillarApi Pillar { get; }
        public PlasmaApi Plasma { get; }
        public SentinelApi Sentinel { get; }
        public StakeApi Stake { get; }
        public SwapApi Swap { get; }
        public TokenApi Token { get; }
        public AcceleratorApi Accelerator { get; }
    }
}
