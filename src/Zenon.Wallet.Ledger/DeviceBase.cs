using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Zenon.Wallet.Ledger
{
    /// <summary>
    /// Base class for all devices
    /// </summary>
    public abstract class DeviceBase : IDevice, IDisposable
    {
        private readonly SemaphoreSlim _WriteAndReadLock = new SemaphoreSlim(1, 1);
        private bool disposed;

        protected ILogger Logger { get; }
        protected ILoggerFactory LoggerFactory { get; }

        public string DeviceId { get; }

        protected DeviceBase(
            string deviceId,
            ILoggerFactory? loggerFactory = null,
            ILogger? logger = null)
        {
            DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
            Logger = logger ?? NullLogger.Instance;
            LoggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
        }

        public abstract Task<TransferResult> ReadAsync(CancellationToken cancellationToken = default);
        public abstract Task<uint> WriteAsync(byte[] data, CancellationToken cancellationToken = default);

        public async Task<TransferResult> WriteAndReadAsync(byte[] writeBuffer, CancellationToken cancellationToken = default)
        {
            if (writeBuffer == null) throw new ArgumentNullException(nameof(writeBuffer));

            await _WriteAndReadLock.WaitAsync(cancellationToken).ConfigureAwait(false);

            using var logScope = Logger.BeginScope("DeviceId: {deviceId} Call: {call} Write Buffer Length: {writeBufferLength}", DeviceId, nameof(WriteAndReadAsync), writeBuffer.Length);

            try
            {
                _ = await WriteAsync(writeBuffer, cancellationToken).ConfigureAwait(false);
                var retVal = await ReadAsync(cancellationToken).ConfigureAwait(false);
                Logger.LogInformation("Successfully called write and read");
                return retVal;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Read/Write Error DeviceId: {DeviceId}");
                throw;
            }
            finally
            {
                _ = _WriteAndReadLock.Release();
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (disposed)
            {
                Logger.LogWarning("Attempted to dispose already disposed device {deviceId}", DeviceId);
                return;
            }

            disposed = true;

            Logger.LogInformation($"{nameof(DeviceBase)}: Disposing... DeviceId: {DeviceId}");

            _WriteAndReadLock.Dispose();

            GC.SuppressFinalize(this);
        }
    }

}
