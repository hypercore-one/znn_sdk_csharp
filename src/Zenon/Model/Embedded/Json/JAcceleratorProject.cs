﻿namespace Zenon.Model.Embedded.Json
{
    public class JAcceleratorProject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string znnFundsNeeded { get; set; }
        public string qsrFundsNeeded { get; set; }
        public long creationTimestamp { get; set; }
        public int status { get; set; }
        public JVoteBreakdown votes { get; set; }
    }
}
