using Zenon.Wallet.Ledger.Requests;
using Zenon.Wallet.Ledger.Responses;

namespace Zenon.Wallet.Ledger
{
    public class LedgerTransport : IHandlesRequest, IDisposable
    {
        private readonly SemaphoreSlim _SemaphoreSlim = new SemaphoreSlim(1, 1);
        private bool _IsDisposed;

        public IDevice LedgerHidDevice { get; }

        public LedgerTransport(IDevice ledgerHidDevice)
        {
            LedgerHidDevice = ledgerHidDevice;
        }

        private async Task<IEnumerable<byte[]>> WriteRequestAndReadAsync<TRequest>(TRequest request) where TRequest : RequestBase
        {
            var responseData = new List<byte[]>();

            var apduChunks = request.ToAPDUChunks();

            for (var i = 0; i < apduChunks.Count; i++)
            {
                var apduCommandChunk = apduChunks[i];

                if (apduChunks.Count == 1)
                {
                    //There is only one chunk so use the argument from the request (e.g P1_START)
                    apduCommandChunk[2] = request.Argument1;
                    apduCommandChunk[3] = Constants.P2_LAST;
                }
                else if (apduChunks.Count > 1)
                {
                    //There are multiple chunks so the assumption is that this is probably a transaction

                    if (i == 0)
                    {
                        //This is the first chunk of the transaction
                        apduCommandChunk[2] = (byte)i;
                        apduCommandChunk[3] = Constants.P2_MORE;
                    }
                    else if (i == apduChunks.Count - 1)
                    {
                        //This is the last chunk of the transaction
                        apduCommandChunk[2] = (byte)i;
                        apduCommandChunk[3] = Constants.P2_LAST;
                    }
                    else
                    {
                        //This is one of the middle chunks and there is more coming
                        apduCommandChunk[2] = (byte)i;
                        apduCommandChunk[3] = Constants.P2_MORE;
                    }
                }

                var packetIndex = 0;
                byte[]? data = null;
                using (var memoryStream = new MemoryStream(apduCommandChunk))
                {
                    do
                    {
                        data = Helpers.GetRequestDataPacket(memoryStream, packetIndex);
                        packetIndex++;
                        await LedgerHidDevice.WriteAsync(data);
                    } while (memoryStream.Position != memoryStream.Length);
                }

                var responseDataChunk = (await ReadAsync())!;

                responseData.Add(responseDataChunk);

                var returnCode = ResponseBase.GetReturnCode(responseDataChunk);

                if (returnCode != Constants.SuccessStatusCode)
                {
                    return responseData;
                }
            }
            return responseData;
        }

        private async Task<byte[]?> ReadAsync()
        {
            var remaining = 0;
            var packetIndex = 0;

            using (var response = new MemoryStream())
            {
                do
                {
                    var packetData = await LedgerHidDevice.ReadAsync();
                    var responseDataPacket = Helpers.GetResponseDataPacket(packetData, packetIndex, ref remaining);
                    packetIndex++;

                    if (responseDataPacket == null)
                    {
                        return null;
                    }

                    await response.WriteAsync(responseDataPacket, 0, responseDataPacket.Length);

                } while (remaining != 0);

                return response.ToArray();
            }
        }

        private async Task<TResponse> SendRequestAsync<TResponse>(RequestBase request)
            where TResponse : ResponseBase
        {
            await _SemaphoreSlim.WaitAsync();

            try
            {
                var responseDataChunks = await WriteRequestAndReadAsync(request);

                return (TResponse)Activator.CreateInstance(typeof(TResponse), responseDataChunks.Last());
            }
            finally
            {
                _SemaphoreSlim.Release();
            }
        }

        public async Task<TResponse> SendRequestAsync<TResponse, TRequest>(TRequest request)
            where TResponse : ResponseBase
            where TRequest : RequestBase
        {
            return await SendRequestAsync<TResponse>(request);
        }

        public void Dispose()
        {
            if (_IsDisposed) return;
            _IsDisposed = true;

            _SemaphoreSlim.Dispose();
            LedgerHidDevice?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
