using Zenon.Wallet.Ledger.Requests;
using Zenon.Wallet.Ledger.Responses;

namespace Zenon.Wallet.Ledger
{
    public interface IHandlesRequest
    {
        Task<TResponse> SendRequestAsync<TResponse, TRequest>(TRequest request)
            where TResponse : ResponseBase
            where TRequest : RequestBase;
    }
}
