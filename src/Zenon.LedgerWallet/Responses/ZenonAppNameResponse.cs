using System.Text;

namespace Zenon.LedgerWallet.Responses
{
    public class ZenonAppNameResponse : ResponseBase
    {
        public byte[]? AppNameData { get; }
        public string? AppName { get; }

        public ZenonAppNameResponse(byte[] data)
            : base(data)
        {
            if (!IsSuccess)
            {
                return;
            }

            AppNameData = data;
            AppName = Encoding.UTF8.GetString(data, 0, data.Length - 2);
        }
    }
}
