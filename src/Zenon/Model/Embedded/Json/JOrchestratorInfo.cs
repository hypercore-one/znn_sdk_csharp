namespace Zenon.Model.Embedded.Json
{
    public class JOrchestratorInfo
    {
        public ulong windowSize { get; set; }
        public uint keyGenThreshold { get; set; }
        public uint confirmationsToFinality { get; set; }
        public uint estimatedMomentumTime { get; set; }
        public ulong allowKeyGenHeight { get; set; }
    }
}