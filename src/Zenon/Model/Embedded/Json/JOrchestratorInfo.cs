namespace Zenon.Model.Embedded.Json
{
    public class JOrchestratorInfo
    {
        public long windowSize { get; set; }
        public long keyGenThreshold { get; set; }
        public long confirmationsToFinality { get; set; }
        public long estimatedMomentumTime { get; set; }
        public long allowKeyGenHeight { get; set; }
    }
}