namespace Zenon.Model.NoM.Json
{
    public class JAccountBlockList
    {
        public long count { get; set; }
        public JAccountBlock[] list { get; set; }
        public bool more { get; set; }
    }
}
