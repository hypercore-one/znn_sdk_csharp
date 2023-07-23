namespace Zenon.Wallet.Ledger
{
    public interface IDevice : IDisposable
    {
        /// <summary>
        /// Read a page of data. Warning: this is not thread safe. WriteAndReadAsync() should be preferred.
        /// </summary>
        Task<TransferResult> ReadAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Write a page of data. Warning: this is not thread safe. WriteAndReadAsync() should be preferred.
        /// </summary>
        Task<uint> WriteAsync(byte[] data, CancellationToken cancellationToken = default);
    }
}
