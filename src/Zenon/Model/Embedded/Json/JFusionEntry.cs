namespace Zenon.Model.Embedded.Json
{
    public class JFusionEntry
    {
        public string qsrAmount { get; set; }
        public string beneficiary { get; set; }
        public ulong expirationHeight { get; set; }
        public string id { get; set; }
        public bool? isRevocable { get; set; }
    }
}
