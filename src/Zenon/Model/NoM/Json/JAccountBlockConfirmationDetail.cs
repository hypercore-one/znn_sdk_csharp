namespace Zenon.Model.NoM.Json
{
    public class JAccountBlockConfirmationDetail
    {
        public ulong numConfirmations { get; set; }
        public ulong momentumHeight { get; set; }
        public string momentumHash { get; set; }
        public ulong momentumTimestamp { get; set; }
    }
}
