namespace Zenon.Model.Embedded.Json
{
    public class JSecurityInfo
    {
        public string[] guardians { get; set; }
        public string[] guardiansVotes { get; set; }
        public ulong administratorDelay { get; set; }
        public ulong softDelay { get; set; }
    }
}
