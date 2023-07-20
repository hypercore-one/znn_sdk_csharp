using Zenon.LedgerWallet.Requests;
using Zenon.LedgerWallet.Responses;

namespace Zenon.LedgerWallet
{
    public interface IHandlesRequest
    {
        Task<TResponse> SendRequestAsync<TResponse, TRequest>(TRequest request)
            where TResponse : ResponseBase
            where TRequest : RequestBase;
    }
}
