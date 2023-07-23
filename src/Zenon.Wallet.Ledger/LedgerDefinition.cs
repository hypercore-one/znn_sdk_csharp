using HidApi;

namespace Zenon.Wallet.Ledger
{
    public class LedgerDefinition : IWalletDefinition
    {
        public LedgerDefinition(DeviceInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            DeviceInfo = info;
        }

        public DeviceInfo DeviceInfo { get; }

        public string WalletId => DeviceInfo.Path;

        public string WalletName => DeviceInfo.ProductString;
    }
}
