namespace Zenon.Wallet
{
    public class IncorrectPasswordException : ZdkException
    {
        private const string IncorrectPassword = "Incorrect password";

        public IncorrectPasswordException()
            : base(IncorrectPassword)
        { }
    }

}
