namespace Zenon.Client
{
    public class NoConnectionException : ZdkException
    {
        public NoConnectionException()
            : base("No connection to the Zenon full node")
        { }
    }
}
