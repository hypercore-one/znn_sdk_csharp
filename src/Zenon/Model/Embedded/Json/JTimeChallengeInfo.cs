namespace Zenon.Model.Embedded.Json
{
    public class JTimeChallengeInfo
    {
        public string methodName { get; set; }
        public string paramsHash { get; set; }
        public ulong challengeStartHeight { get; set; }
    }
}