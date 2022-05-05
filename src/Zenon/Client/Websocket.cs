using StreamJsonRpc;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Zenon.Client
{
    public enum WebsocketStatus
    {
        Uninitialized,
        Connecting,
        Running,
        Stopped
    }

    public class WsClient : IClient
    {
        public WsClient(Uri url)
        {
            Url = url;
            Status = WebsocketStatus.Uninitialized;
        }

        private ClientWebSocket _socket;
        private JsonRpc _wsRpcClient;

        public Uri Url { get; }

        public WebsocketStatus Status { get; private set; }

        public bool IsClosed => _wsRpcClient == null || _wsRpcClient.IsDisposed;

        public async Task<T> SendRequest<T>(string method, params object[] parameters)
        {
            if (IsClosed)
            {
                throw new NoConnectionException();
            }

            return await _wsRpcClient.InvokeAsync<T>(method, parameters);
        }

        public async Task SendRequest(string method, params object[] parameters)
        {
            if (IsClosed)
            {
                throw new NoConnectionException();
            }

            await _wsRpcClient.InvokeAsync(method, parameters);
        }

        public async Task<bool> Start(bool rety = true)
        {
            Debug.WriteLine("Initializing websocket connection ...");

            Status = WebsocketStatus.Connecting;

            do
            {
                _socket = new ClientWebSocket();
                _socket.Options.KeepAliveInterval = TimeSpan.FromSeconds(5);

                try
                {
                    await _socket.ConnectAsync(Url, CancellationToken.None);

                    Debug.WriteLine("Websocket connection successfully established");

                    _wsRpcClient = new JsonRpc(new WebSocketMessageHandler(_socket));

                    Status = WebsocketStatus.Running;

                    _wsRpcClient.StartListening();

                    return true;
                }
                catch (SocketException)
                {
                    if (rety)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
            }
            while (rety);

            return false;
        }

        public async Task Stop()
        {
            if (Status != WebsocketStatus.Running)
            {
                return;
            }
            Status = WebsocketStatus.Stopped;

            Debug.WriteLine("Websocket client closed");

            if (_socket != null)
            {
                try
                {
                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
                }
                catch { }
                _socket = null;
            }

            if (_wsRpcClient != null)
            {
                try
                {
                    _wsRpcClient.Dispose();
                }
                catch { }
                _wsRpcClient = null;
            }

            Debug.WriteLine("Websocket client is now closed");
        }
    }
}