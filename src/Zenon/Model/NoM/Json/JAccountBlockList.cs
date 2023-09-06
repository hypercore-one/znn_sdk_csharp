namespace Zenon.Model.NoM.Json
{
    public class JAccountBlockList
    {
        public JAccountBlock[] list { get; set; }
        public ulong count { get; set; }
        public bool more { get; set; }
    }
}
