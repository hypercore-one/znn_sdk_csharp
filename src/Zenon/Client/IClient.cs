using System.Threading.Tasks;

namespace Zenon.Client
{
    public interface IClient
    {
        Task<T> SendRequest<T>(string method, params object[] parameters);

        Task SendRequest(string method, params object[] parameters);
    }
}
