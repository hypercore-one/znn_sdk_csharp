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
        public WsClient()
        {
            Status = WebsocketStatus.Uninitialized;
        }

        private ClientWebSocket _socket;
        private JsonRpc _wsRpcClient;

        public Uri Url { get; private set; }

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

        public async Task<bool> StartAsync(Uri url, bool retry = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.Url = url;

            Debug.WriteLine("Initializing websocket connection ...");

            this.Status = WebsocketStatus.Connecting;

            do
            {
                _socket = new ClientWebSocket();
                _socket.Options.KeepAliveInterval = TimeSpan.FromSeconds(5);

                try
                {
                    await _socket.ConnectAsync(Url, cancellationToken);

                    Debug.WriteLine("Websocket connection successfully established");

                    _wsRpcClient = new JsonRpc(new WebSocketMessageHandler(_socket));

                    this.Status = WebsocketStatus.Running;

                    _wsRpcClient.StartListening();

                    return true;
                }
                catch (SocketException)
                {
                    if (retry)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
                catch (OperationCanceledException)
                {
                    retry = false;

                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
                }
            }
            while (retry);

            return false;
        }

        public async Task StopAsync()
        {
            if (this.Status != WebsocketStatus.Running)
            {
                return;
            }
            this.Status = WebsocketStatus.Stopped;

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