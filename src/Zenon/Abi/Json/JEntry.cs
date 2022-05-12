namespace Zenon.Abi.Json
{
    public class JEntry
    {
        public string name { get; set; }
        public string type { get; set; }
        public JParam[] inputs { get; set; }
    }
}
