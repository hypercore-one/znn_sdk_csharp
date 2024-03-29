﻿using Zenon.Api.Embedded;
using Zenon.Client;

namespace Zenon.Api
{
    public class EmbeddedApi
    {
        public EmbeddedApi(IClient client)
        {
            Client = client;

            Pillar = new PillarApi(client);
            Plasma = new PlasmaApi(client);
            Sentinel = new SentinelApi(client);
            Stake = new StakeApi(client);
            Swap = new SwapApi(client);
            Token = new TokenApi(client);
            Accelerator = new AcceleratorApi(client);
            Spork = new SporkApi(client);
            Htlc = new HtlcApi(client);
            Liquidity = new LiquidityApi(client);
            Bridge = new BridgeApi(client);
        }

        public IClient Client { get; }

        public PillarApi Pillar { get; }
        public PlasmaApi Plasma { get; }
        public SentinelApi Sentinel { get; }
        public StakeApi Stake { get; }
        public SwapApi Swap { get; }
        public TokenApi Token { get; }
        public AcceleratorApi Accelerator { get; }
        public SporkApi Spork { get; }
        public HtlcApi Htlc { get; }
        public LiquidityApi Liquidity { get; }
        public BridgeApi Bridge { get; }
    }
}
