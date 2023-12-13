using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zenon.Client;

namespace Zenon.Api
{
    public class TestClientRequest
    {
        public TestClientRequest(string methodName, params object[] parameters)
        {
            this.MethodName = methodName;
            this.Parameters = parameters;
        }

        public string MethodName { get; }
        public object[] Parameters { get; }

        public void Validate(string methodName, params object[] parameters)
        {
            methodName.Should().Be(this.MethodName);
            parameters.Should().BeEquivalentTo(this.Parameters);
        }
    }

    public class TestClient : IClient
    {
        public TestClient()
        {
            this.Response = () => null;
        }

        public TestClientRequest Request { get; private set; }
        public Func<string> Response { get; private set; }

        public int ProtocolVersion => Constants.ProtocolVersion;

        public int ChainIdentifier => Constants.ChainId;

        public async Task<T> SendRequestAsync<T>(string method, params object[] parameters)
        {
            return await Task.Run(() =>
            {
                this.Request?.Validate(method, parameters);

                return JsonConvert.DeserializeObject<T>(this.Response());
            });
        }

        public async Task SendRequestAsync(string method, params object[] parameters)
        {
            await Task.Run(() =>
            {
                this.Request?.Validate(method, parameters);

                this.Response();
            });
        }

        public Task<bool> StartAsync(Uri url, bool retry = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }

        public void Subscribe(string method, Delegate callback)
        {
            throw new NotImplementedException();
        }

        public TestClient WithMethod(string methodName, params object[] parameters)
        {
            this.Request = new TestClientRequest(methodName, parameters);
            return this;
        }

        public TestClient WithResponse(Func<string> response)
        {
            this.Response = response;
            return this;
        }

        public TestClient WithNullResponse()
        {
            this.Response = () => "null";
            return this;
        }

        public TestClient WithEmptyResponse()
        {
            this.Response = () => "{}";
            return this;
        }

        public TestClient WithExceptionResponse(Exception ex)
        {
            this.Response = () => throw ex;
            return this;
        }

        public TestClient WithManifestResourceTextResponse(string resourceName)
        {
            this.Response = () => TestHelper.GetManifestResourceText(resourceName);
            return this;
        }
    }
}
