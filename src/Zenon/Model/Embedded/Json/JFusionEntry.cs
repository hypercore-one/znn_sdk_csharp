namespace Zenon.Model.Embedded.Json
{
    public class JFusionEntry
    {
        public long qsrAmount { get; set; }
        public string beneficiary { get; set; }
        public long expirationHeight { get; set; }
        public string id { get; set; }
        public bool? isRevocable { get; set; }
    }
}
