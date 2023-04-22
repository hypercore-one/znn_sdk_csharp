namespace Zenon.Model.NoM.Json
{
    public class JAccountBlockList
    {
        public JAccountBlock[] list { get; set; }
        public long count { get; set; }
        public bool more { get; set; }
    }
}
