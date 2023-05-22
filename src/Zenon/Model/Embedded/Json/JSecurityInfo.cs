namespace Zenon.Model.Embedded.Json
{
    public class JSecurityInfo
    {
        public string[] guardians { get; set; }
        public string[] guardiansVotes { get; set; }
        public long administratorDelay { get; set; }
        public long softDelay { get; set; }
    }
}
