namespace Zenon.Model.Embedded.Json
{
    public class JFusionEntryList
    {
        public string qsrAmount { get; set; }
        public ulong count { get; set; }
        public JFusionEntry[] list { get; set; }
    }
}
