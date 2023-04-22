namespace Zenon.Wallet
{
    public class IncorrectPasswordException : ZnnSdkException
    {
        private const string IncorrectPassword = "Incorrect password";

        public IncorrectPasswordException()
            : base(IncorrectPassword)
        { }
    }

}
