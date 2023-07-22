using StreamJsonRpc;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection.Metadata;
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

    public class WsClient : IClient, IDisposable
    {
        public static readonly WsClientOptions DefaultOptions = new WsClientOptions()
        {
            ProtocolVersion = Constants.ProtocolVersion,
            ChainIdentifier = Constants.ChainId,
            TraceSourceLevels = SourceLevels.Warning
        };

        private bool disposed;
        private ClientWebSocket socket;
        private JsonRpc wsRpcClient;

        public WsClient(string url)
            : this(url, DefaultOptions)
        { }

        public WsClient(string url, WsClientOptions options)
        {
            if (!Utils.ValidateWsConnectionrUrl(url))
                throw new ArgumentException("Invalid url");
            Url = new Uri(url);
            options ??= DefaultOptions;
            ProtocolVersion = options.ProtocolVersion;
            ChainIdentifier = options.ChainIdentifier;
            TraceSourceLevels = options.TraceSourceLevels;
        }

        public Uri Url { get; }

        public int ProtocolVersion { get; }

        public int ChainIdentifier { get; }

        public SourceLevels TraceSourceLevels { get; }

        public WebsocketStatus Status { get; private set; }

        public bool IsClosed => wsRpcClient == null || wsRpcClient.IsDisposed;

        public bool IsDisposed => disposed;

        public async Task<T> SendRequestAsync<T>(string method, params object[] parameters)
        {
            AssertConnection();

            return await wsRpcClient.InvokeAsync<T>(method, parameters);
        }

        public async Task SendRequestAsync(string method, params object[] parameters)
        {
            AssertConnection();

            await wsRpcClient.InvokeAsync(method, parameters);
        }

        public void Subscribe(string method, Delegate callback)
        {
            AssertConnection();

            wsRpcClient.AllowModificationWhileListening = true;
            wsRpcClient.AddLocalRpcMethod(method, callback);
            wsRpcClient.AllowModificationWhileListening = false;
        }

        public async Task<bool> ConnectAsync(bool retry = true, CancellationToken cancellationToken = default)
        {
            AssertDisposed();

            Debug.WriteLine("Initializing websocket connection ...");

            this.Status = WebsocketStatus.Connecting;

            do
            {
                socket = new ClientWebSocket();
                socket.Options.KeepAliveInterval = TimeSpan.FromSeconds(5);

                try
                {
                    await socket.ConnectAsync(Url, cancellationToken);

                    Debug.WriteLine("Websocket connection successfully established");

                    wsRpcClient = new JsonRpc(new WebSocketMessageHandler(socket));
                    wsRpcClient.TraceSource.Listeners.Add(new ConsoleTraceListener());
                    wsRpcClient.TraceSource.Switch.Level = TraceSourceLevels;

                    this.Status = WebsocketStatus.Running;

                    wsRpcClient.StartListening();

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

                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
                }
            }
            while (retry);

            return false;
        }

        public async Task CloseAsync()
        {
            AssertDisposed();

            if (this.Status != WebsocketStatus.Running)
            {
                return;
            }
            this.Status = WebsocketStatus.Stopped;

            Debug.WriteLine("Websocket client closed");

            if (socket != null)
            {
                try
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
                }
                catch { }
                socket = null;
            }

            if (wsRpcClient != null)
            {
                try
                {
                    wsRpcClient.Dispose();
                }
                catch { }
                wsRpcClient = null;
            }

            Debug.WriteLine("Websocket client is now closed");
        }

        private void AssertConnection()
        {
            if (IsClosed)
                throw new NoConnectionException();
        }

        private void AssertDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(WsClient));
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                if (socket != null)
                {
                    try
                    {
                        socket.Dispose();
                    }
                    catch { }
                    socket = null;
                }
                
                if (wsRpcClient != null)
                {
                    try
                    {
                        wsRpcClient.Dispose();
                    }
                    catch { }
                    wsRpcClient = null;
                }

                disposed = true;
            }
        }
    }
}