namespace Zenon.Wallet.Ledger.Requests
{
    public abstract class AddressPathRequestBase : RequestBase
    {
        public IAddressPath AddressPath { get; }

        public AddressPathRequestBase(IAddressPath path, byte[] data)
            : base(data)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            AddressPath = path;
        }

        internal override List<byte[]> ToAPDUChunks()
        {
            var retVal = new List<byte[]>();

            var offset = 0;

            if (Data.Length > 0)
            {
                retVal.Add(GetNextApduCommand(
                    Helpers.GetDerivationPathData(AddressPath), ref offset));

                offset = 0;

                while (offset < Data.Length - 1)
                {
                    retVal.Add(GetNextApduCommand(Data, ref offset));
                }

                return retVal;
            }
            else
            {
                retVal.Add(GetNextApduCommand(
                    Helpers.GetDerivationPathData(AddressPath), ref offset));
            }

            return retVal;
        }
    }
}
