using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zenon.Client
{
    public interface IClient
    {
        Task<T> SendRequest<T>(string method, params object[] parameters);

        Task SendRequest(string method, params object[] parameters);

        Task<bool> StartAsync(Uri url, bool retry = true, CancellationToken cancellationToken = default(CancellationToken));

        Task StopAsync();

        void Subscribe(string method, Delegate callback);
    }
}
