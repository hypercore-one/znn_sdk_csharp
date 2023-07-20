namespace Zenon.LedgerWallet.Requests
{
    public abstract class RequestBase
    {
        public abstract byte Argument1 { get; }
        public abstract byte Argument2 { get; }
        public abstract byte Cla { get; }
        public abstract byte Ins { get; }

        public byte[] Data { get; }

        protected RequestBase(byte[] data)
        {
            Data = data;
        }

        protected byte[] GetNextApduCommand(byte[] data, ref int offset)
        {
            var chunkSize = offset + Constants.LEDGER_MAX_DATA_SIZE > data.Length ? data.Length - offset : Constants.LEDGER_MAX_DATA_SIZE;

            var buffer = new byte[5 + chunkSize];
            buffer[0] = Cla;
            buffer[1] = Ins;
            //buffer[2] will be filled in later when we know how many chunks there are
            buffer[3] = Argument2;
            buffer[4] = (byte)chunkSize;
            Array.Copy(data, offset, buffer, 5, chunkSize);

            offset += chunkSize;
            return buffer;
        }

        internal virtual List<byte[]> ToAPDUChunks()
        {
            var offset = 0;

            if (Data.Length > 0)
            {
                var retVal = new List<byte[]>();

                while (offset < Data.Length - 1)
                {
                    retVal.Add(GetNextApduCommand(Data, ref offset));
                }

                return retVal;
            }
            else
            {
                return new List<byte[]> { GetNextApduCommand(Data, ref offset) };
            }
        }
    }
}
