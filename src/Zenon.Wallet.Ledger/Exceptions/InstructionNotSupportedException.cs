namespace Zenon.Wallet.Ledger.Exceptions
{
    public class InstructionNotSupportedException : ResponseBaseException
    {
        public InstructionNotSupportedException(byte[] responseData, int returnCode)
            : base("The instruction sent to the device is not supported. This probably means that the instruction sent to the device is not implemented by the app that is currently loaded on the Ledger.", responseData, returnCode)
        {
        }
    }
}
