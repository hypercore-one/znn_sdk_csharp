﻿namespace Zenon.Model.Embedded.Json
{
    public class JTokenPair
    {
        public string tokenStandard { get; set; }
        public string tokenAddress { get; set; }
        public bool bridgeable { get; set; }
        public bool redeemable { get; set; }
        public bool owned { get; set; }
        public string minAmount { get; set; }
        public uint feePercentage { get; set; }
        public ulong redeemDelay { get; set; }
        public string metadata { get; set; }
    }
}