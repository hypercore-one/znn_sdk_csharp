using System;
using System.Threading.Tasks;

namespace Zenon.Client
{
    public interface IClient
    {
        int ProtocolVersion { get; }

        int ChainIdentifier { get; }

        Task SendRequestAsync(string method, params object[] parameters);

        Task<T> SendRequestAsync<T>(string method, params object[] parameters);

        void Subscribe(string method, Delegate callback);
    }
}
