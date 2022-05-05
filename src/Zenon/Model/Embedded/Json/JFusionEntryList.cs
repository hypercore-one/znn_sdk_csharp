namespace Zenon.Model.Embedded.Json
{
    public class JFusionEntryList
    {
        public long qsrAmount { get; set; }
        public long count { get; set; }
        public JFusionEntry[] list { get; set; }
    }
}
