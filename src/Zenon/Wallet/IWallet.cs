using System.Threading.Tasks;

namespace Zenon.Wallet
{
    public interface IWallet
    {
        Task<ISigner> GetSignerAsync(int index = 0);
    }
}
