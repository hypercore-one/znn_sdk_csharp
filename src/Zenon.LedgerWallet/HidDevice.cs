using HidApi;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Zenon.LedgerWallet
{
    public class HidDevice : DeviceBase
    {
        private bool disposed;

        public HidDevice(string path, string deviceId, ILoggerFactory? loggerFactory = null)
            : base(deviceId, loggerFactory, (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<HidDevice>())
        {
            Device = new Device(path);
        }

        public HidDevice(ushort vendorId, ushort productId, string deviceId, ILoggerFactory? loggerFactory = null)
            : base(deviceId, loggerFactory, (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<HidDevice>())
        {
            Device = new Device(vendorId, productId);
        }

        public HidDevice(ushort vendorId, ushort productId, string serialNumber, string deviceId, ILoggerFactory? loggerFactory = null)
            : base(deviceId, loggerFactory, (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<HidDevice>())
        {
            Device = new Device(vendorId, productId, serialNumber);
        }

        private Device Device { get; }

        public override async Task<TransferResult> ReadAsync(CancellationToken cancellationToken = default)
        {
            AssertDisposed();

            return await Task.Run(() =>
            {
                var data = Device.Read(0xFF).ToArray();
                return new TransferResult(data, (uint)data.Length);
            });
        }

        public override async Task<uint> WriteAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            AssertDisposed();

            return await Task.Run<uint>(() =>
            {
                var buffer = new byte[data.Length + 1];
                System.Buffer.BlockCopy(data, 0, buffer, 1, data.Length);

                Device.Write(buffer);

                return (uint)buffer.Length;
            });
        }

        private void AssertDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(HidDevice));
        }

        protected override void Dispose(bool dispose)
        {
            if (disposed)
                return;

            disposed = true;

            base.Dispose(dispose);

            Device.Dispose();
        }
    }
}
