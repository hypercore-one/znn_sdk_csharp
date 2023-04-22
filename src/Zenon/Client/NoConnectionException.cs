namespace Zenon.Client
{
    public class NoConnectionException : ZnnSdkException
    {
        public NoConnectionException()
            : base("No connection to the Zenon full node")
        { }
    }
}
