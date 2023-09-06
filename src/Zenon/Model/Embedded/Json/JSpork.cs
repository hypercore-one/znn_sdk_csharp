namespace Zenon.Model.Embedded.Json
{
    public class JSpork
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool activated { get; set; }
        public ulong enforcementHeight { get; set; }
    }
}
