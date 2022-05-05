namespace Zenon.Model.Embedded.Json
{
    public class JProject : JAcceleratorProject
    {
        public string owner { get; set; }
        public string[] phaseIds { get; set; }
        public JPhase[] phases { get; set; }
        public long lastUpdateTimestamp { get; set; }
    }
}
