using System.Threading.Tasks;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Wallet
{
    public interface ISigner
    {
        Task<Address> GetAddressAsync();

        Task<byte[]> GetPublicKeyAsync();

        Task<byte[]> SignAsync(byte[] message);

        Task<byte[]> SignTxAsync(AccountBlockTemplate tx);
    }
}
