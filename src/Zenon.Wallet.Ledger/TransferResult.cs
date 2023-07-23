#pragma warning disable CA1815 // Override equals and operator equals on value types


namespace Zenon.Wallet.Ledger
{
    /// <summary>
    /// Represents the result of a read or write transfer
    /// </summary>
    public readonly struct TransferResult
    {
        public TransferResult(byte[] data, uint bytesRead)
        {
            Data = data;
            BytesTransferred = bytesRead;
        }

        /// <summary>
        /// The data that was transferred
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// The number of bytes transferred
        /// </summary>
        public uint BytesTransferred { get; }

        public static implicit operator byte[](TransferResult TransferResult) => TransferResult.Data;

        public override string ToString() => $"Bytes transferred: {BytesTransferred}\r\n{string.Join(", ", Data)}";
    }
}
