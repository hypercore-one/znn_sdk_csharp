using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zenon.Api;
using Zenon.Api.Embedded;
using Zenon.Client;
using Zenon.Model.Embedded;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Tests
{
    #region TestClient
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

        public async Task<T> SendRequest<T>(string method, params object[] parameters)
        {
            return await Task.Run(() =>
            {
                this.Request?.Validate(method, parameters);

                return JsonConvert.DeserializeObject<T>(this.Response());
            });
        }

        public async Task SendRequest(string method, params object[] parameters)
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
    #endregion

    public partial class ApiTests
    {
        #region Embedded
        public class Accelerator
        {
            public class GetAll
            {
                public GetAll()
                {
                    this.MethodName = "embedded.accelerator.getAll";
                }

                public string MethodName { get; }

                [Fact]
                public async Task EmptyResponseAsync()
                {
                    // Setup
                    var api = new AcceleratorApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, 0, Constants.RpcMaxPageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAll();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Tests.TestData.Api.Embedded.Accelerator.GetAll.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new AcceleratorApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAll();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetProjectById
            {
                public GetProjectById()
                {
                    this.MethodName = "embedded.accelerator.getProjectById";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("2acf48da2bcc420b4d997299807d8b50b4229871214069c9afc39bdad0e27a76",
                    "Zenon.Tests.TestData.Api.Embedded.Accelerator.GetProjectById.json")]
                public async Task SingleResponseAsync(string id, string resourceName)
                {
                    // Setup
                    var hash = Hash.Parse(id);
                    var api = new AcceleratorApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, hash.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetProjectById(hash);

                    // Validate
                    result.Should().NotBeNull();
                    result.Id.Should().Be(hash);
                }
            }

            public class GetPhaseById
            {
                public GetPhaseById()
                {
                    this.MethodName = "embedded.accelerator.getPhaseById";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("54946aa46c4f8d4bb5a98b2aca8e90311d9388f2d47ab754ad012cb5cd40448d",
                    "Zenon.Tests.TestData.Api.Embedded.Accelerator.GetPhaseById.json")]
                public async Task SingleResponseAsync(string id, string resourceName)
                {
                    // Setup
                    var hash = Hash.Parse(id);
                    var api = new AcceleratorApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, hash.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetPhaseById(hash);

                    // Validate
                    result.Should().NotBeNull();
                    result.Id.Should().Be(hash);
                }
            }

            public class GetPillarVotes
            {
                public GetPillarVotes()
                {
                    this.MethodName = "embedded.accelerator.getPillarVotes";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("", new string[0],
                    "Zenon.Tests.TestData.Api.Embedded.Accelerator.GetPillarVotes.json")]
                public async Task ArrayResponseAsync(string name, string[] hashes, string resourceName)
                {
                    // Setup
                    var api = new AcceleratorApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, name, hashes)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetPillarVotes(name, hashes);

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetVoteBreakdown
            {
                public GetVoteBreakdown()
                {
                    this.MethodName = "embedded.accelerator.getVoteBreakdown";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("54946aa46c4f8d4bb5a98b2aca8e90311d9388f2d47ab754ad012cb5cd40448d",
                    "Zenon.Tests.TestData.Api.Embedded.Accelerator.GetVoteBreakdown.json")]
                public async Task SingleResponseAsync(string id, string resourceName)
                {
                    // Setup
                    var hash = Hash.Parse(id);
                    var api = new AcceleratorApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, hash.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetVoteBreakdown(hash);

                    // Validate
                    result.Should().NotBeNull();
                    result.Id.Should().Be(hash);
                }
            }
        }

        public class Pillar
        {
            public class GetDepositedQsr
            {
                public GetDepositedQsr()
                {
                    this.MethodName = "embedded.pillar.getDepositedQsr";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqz642m6sk9qpa4896etljzv6phlspmx3ctkl6", "0")]
                public async Task SingleResponseAsync(string address, string response)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithResponse(() => response)));

                    // Execute
                    var result = await api.GetDepositedQsr(addr);

                    // Validate
                    result.Should().Be(0);
                }
            }

            public class GetUncollectedReward
            {
                public GetUncollectedReward()
                {
                    this.MethodName = "embedded.pillar.getUncollectedReward";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qprz0pjd7tykyxsycr2l4escty4nm2p5f8ct6f",
                    "Zenon.Tests.TestData.Api.Embedded.Pillar.GetUncollectedReward.json")]
                public async Task SingleResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetUncollectedReward(addr);

                    // Validate
                    result.Should().NotBeNull();
                    result.Address.Should().Be(addr);
                }
            }

            public class GetFrontierRewardByPage
            {
                public GetFrontierRewardByPage()
                {
                    this.MethodName = "embedded.pillar.getFrontierRewardByPage";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetFrontierRewardByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Pillar.GetFrontierRewardByPage.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetFrontierRewardByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetQsrRegistrationCost
            {
                public GetQsrRegistrationCost()
                {
                    this.MethodName = "embedded.pillar.getQsrRegistrationCost";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("25000000000000")]
                public async Task SingleResponseAsync(string response)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithResponse(() => response)));

                    // Execute
                    var result = await api.GetQsrRegistrationCost();

                    // Validate
                    result.Should().Be(25000000000000);
                }
            }

            public class GetAll
            {
                public GetAll()
                {
                    this.MethodName = "embedded.pillar.getAll";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAll(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Pillar.GetAll.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAll(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetByOwner
            {
                public GetByOwner()
                {
                    this.MethodName = "embedded.pillar.getByOwner";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qzlaadsmar8pm0rdfwkctvxc8n2g5gaadxvmqj")]
                public async Task EmptyResponseAsync(string address)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithResponse(() => "[]")));

                    // Execute
                    var result = await api.GetByOwner(addr);

                    // Validate
                    result.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qzlaadsmar8pm0rdfwkctvxc8n2g5gaadxvmqj",
                    "Zenon.Tests.TestData.Api.Embedded.Pillar.GetByOwner.json")]
                public async Task ArrayResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetByOwner(addr);

                    // Validate
                    result.Should().NotBeNullOrEmpty();
                }
            }

            public class GetByName
            {
                public GetByName()
                {
                    this.MethodName = "embedded.pillar.getByName";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("SultanOfStaking",
                    "Zenon.Tests.TestData.Api.Embedded.Pillar.GetByName.json")]
                public async Task SingleResponseAsync(string name, string resourceName)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, name)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetByName(name);

                    // Validate
                    result.Should().NotBeNull();
                    result.Name.Should().Be(name);
                }
            }

            public class CheckNameAvailability
            {
                public CheckNameAvailability()
                {
                    this.MethodName = "embedded.pillar.checkNameAvailability";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("AllYourPillarBelongToUs", "true")]
                public async Task SingleResponseAsync(string name, string response)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, name)
                        .WithResponse(() => response)));

                    // Execute
                    var result = await api.CheckNameAvailability(name);

                    // Validate
                    result.Should().BeTrue();
                }
            }

            public class GetDelegatedPillar
            {
                public GetDelegatedPillar()
                {
                    this.MethodName = "embedded.pillar.getDelegatedPillar";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qzlaadsmar8pm0rdfwkctvxc8n2g5gaadxvmqj",
                    "Zenon.Tests.TestData.Api.Embedded.Pillar.GetDelegatedPillar.json")]
                public async Task SingleResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, address.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetDelegatedPillar(addr);

                    // Validate
                    result.Should().NotBeNull();
                    result.Name.Should().NotBeNullOrEmpty();
                }
            }

            public class GetPillarEpochHistory
            {
                public GetPillarEpochHistory()
                {
                    this.MethodName = "embedded.pillar.getPillarEpochHistory";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("SultanOfStaking", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string name, int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, name, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetPillarEpochHistory(name, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("SultanOfStaking", 0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Pillar.GetPillarEpochHistory.json")]
                public async Task ListResponseAsync(string name, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, name, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetPillarEpochHistory(name, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetPillarsHistoryByEpoch
            {
                public GetPillarsHistoryByEpoch()
                {
                    this.MethodName = "embedded.pillar.getPillarsHistoryByEpoch";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(140, 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int epoch, int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, epoch, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetPillarsHistoryByEpoch(epoch, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(140, 0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Pillar.GetPillarsHistoryByEpoch.json")]
                public async Task ListResponseAsync(int epoch, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new PillarApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, epoch, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetPillarsHistoryByEpoch(epoch, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }
        }

        public class Plasma
        {
            public class Get
            {
                public Get()
                {
                    this.MethodName = "embedded.plasma.get";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqylpfyyrj5t8pe99n4nf4x634vxse96fg7sge",
                    "Zenon.Tests.TestData.Api.Embedded.Plasma.Get.json")]
                public async Task SingleResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PlasmaApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.Get(addr);

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetEntriesByAddress
            {
                public GetEntriesByAddress()
                {
                    this.MethodName = "embedded.plasma.getEntriesByAddress";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qqylpfyyrj5t8pe99n4nf4x634vxse96fg7sge", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PlasmaApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetEntriesByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qqylpfyyrj5t8pe99n4nf4x634vxse96fg7sge", 0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Plasma.GetEntriesByAddress.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new PlasmaApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetEntriesByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetPlasmaByQsr
            {
                [Theory]
                [InlineData(100.21, 210441)]
                public async Task SingleResponseAsync(double qsrAmount, long expectedResult)
                {
                    // Setup
                    var api = new PlasmaApi(new Lazy<IClient>(() => new TestClient()));

                    // Execute
                    var result = await api.GetPlasmaByQsr(qsrAmount);

                    // Validate
                    result.Should().Be(expectedResult);
                }
            }

            public class GetRequiredPoWForAccountBlock
            {
                public GetRequiredPoWForAccountBlock()
                {
                    this.MethodName = "embedded.plasma.getRequiredPoWForAccountBlock";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qqylpfyyrj5t8pe99n4nf4x634vxse96fg7sge", BlockTypeEnum.UserSend, "z1qzawsrc0mu5kz7lvrfj2mmr4scuernadycqz30",
                    new byte[0], "Zenon.Tests.TestData.Api.Embedded.Plasma.GetRequiredPoWForAccountBlock.json")]
                public async Task SingleResponseAsync(string address, BlockTypeEnum blockType, string toAddress, byte[] data, string resourceName)
                {
                    // Setup
                    var param = new GetRequiredParam(Address.Parse(address), blockType, Address.Parse(toAddress), data);
                    var api = new PlasmaApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, param.ToJson())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetRequiredPoWForAccountBlock(param);

                    // Validate
                    result.Should().NotBeNull();
                }
            }
        }

        public class Sentinel
        {
            public class GetAllActive
            {
                public GetAllActive()
                {
                    this.MethodName = "embedded.sentinel.getAllActive";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new SentinelApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAllActive(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Sentinel.GetAllActive.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new SentinelApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAllActive(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetByOwner
            {
                public GetByOwner()
                {
                    this.MethodName = "embedded.sentinel.getByOwner";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqz642m6sk9qpa4896etljzv6phlspmx3ctkl6",
                    "Zenon.Tests.TestData.Api.Embedded.Sentinel.GetByOwner.json")]
                public async Task SingleResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new SentinelApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetByOwner(addr);

                    // Validate
                    result.Should().NotBeNull();
                    result.Owner.Should().Be(addr);
                }
            }

            public class GetDepositedQsr
            {
                public GetDepositedQsr()
                {
                    this.MethodName = "embedded.sentinel.getDepositedQsr";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqz642m6sk9qpa4896etljzv6phlspmx3ctkl6", "0")]
                public async Task SingleResponseAsync(string address, string response)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new SentinelApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithResponse(() => response)));

                    // Execute
                    var result = await api.GetDepositedQsr(addr);

                    // Validate
                    result.Should().Be(0);
                }
            }

            public class GetUncollectedReward
            {
                public GetUncollectedReward()
                {
                    this.MethodName = "embedded.sentinel.getUncollectedReward";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qrg4nt69dakvw9wc3pmcvnuax70uj04wt4dxk4",
                    "Zenon.Tests.TestData.Api.Embedded.Sentinel.GetUncollectedReward.json")]
                public async Task SingleResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new SentinelApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetUncollectedReward(addr);

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetFrontierRewardByPage
            {
                public GetFrontierRewardByPage()
                {
                    this.MethodName = "embedded.sentinel.getFrontierRewardByPage";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qrg4nt69dakvw9wc3pmcvnuax70uj04wt4dxk4", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new SentinelApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetFrontierRewardByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qrg4nt69dakvw9wc3pmcvnuax70uj04wt4dxk4", 0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Sentinel.GetFrontierRewardByPage.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new SentinelApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetFrontierRewardByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }
        }

        public class Stake
        {
            public class GetEntriesByAddress
            {
                public GetEntriesByAddress()
                {
                    this.MethodName = "embedded.stake.getEntriesByAddress";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new StakeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetEntriesByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Stake.GetEntriesByAddress.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new StakeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetEntriesByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetFrontierRewardByPage
            {
                public GetFrontierRewardByPage()
                {
                    this.MethodName = "embedded.stake.getFrontierRewardByPage";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new StakeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetFrontierRewardByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Stake.GetFrontierRewardByPage.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new StakeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetFrontierRewardByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetUncollectedReward
            {
                public GetUncollectedReward()
                {
                    this.MethodName = "embedded.stake.getUncollectedReward";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y",
                    "Zenon.Tests.TestData.Api.Embedded.Stake.GetUncollectedReward.json")]
                public async Task SingleResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new StakeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetUncollectedReward(addr);

                    // Validate
                    result.Should().NotBeNull();
                    result.Address.Should().Be(addr);
                }
            }
        }

        public class Swap
        {
            public class GetAssets
            {
                public GetAssets()
                {
                    this.MethodName = "embedded.swap.getAssets";
                }

                public string MethodName { get; }

                [Fact]
                public async Task EmptyResponseAsync()
                {
                    // Setup
                    var api = new SwapApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAssets();

                    // Validate
                    result.Should().BeEmpty();
                }

                [Theory]
                [InlineData("Zenon.Tests.TestData.Api.Embedded.Swap.GetAssets.json")]
                public async Task ArrayResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new SwapApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAssets();

                    // Validate
                    result.Should().NotBeEmpty();
                }
            }

            public class GetAssetsByKeyIdHash
            {
                public GetAssetsByKeyIdHash()
                {
                    this.MethodName = "embedded.swap.getAssetsByKeyIdHash";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("ee383852b44821e633bb46d9eb1d6e62a5629b5c78c7a1f4958026c4bbef1d41",
                    "Zenon.Tests.TestData.Api.Embedded.Swap.GetAssetsByKeyIdHash.json")]
                public async Task SingleResponseAsync(string keyIdHash, string resourceName)
                {
                    // Setup
                    var hash = Hash.Parse(keyIdHash);
                    var api = new SwapApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, hash.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAssetsByKeyIdHash(hash);

                    // Validate
                    result.Should().NotBeNull();
                    result.KeyIdHash.Should().Be(hash);
                }
            }

            public class GetLegacyPillars
            {
                public GetLegacyPillars()
                {
                    this.MethodName = "embedded.swap.getLegacyPillars";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("Zenon.Tests.TestData.Api.Embedded.Swap.GetLegacyPillars.json")]
                public async Task ArrayResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new SwapApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetLegacyPillars();

                    // Validate
                    result.Should().NotBeEmpty();
                }
            }
        }

        public class Token
        {
            public class GetAll
            {
                public GetAll()
                {
                    this.MethodName = "embedded.token.getAll";
                }

                public string MethodName { get; }

                [Fact]
                public async Task EmptyResponseAsync()
                {
                    // Setup
                    var api = new TokenApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, 0, Constants.RpcMaxPageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAll();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Tests.TestData.Api.Embedded.Token.GetAll.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new TokenApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAll();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetByOwner
            {
                public GetByOwner()
                {
                    this.MethodName = "embedded.token.getByOwner";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qpgdtn89u9365jr7ltdxu29fy52pnzwe4fl7zc", 0, Constants.RpcMaxPageSize,
                    "Zenon.Tests.TestData.Api.Embedded.Token.GetByOwner.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new TokenApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetByOwner(addr);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetByZts
            {
                public GetByZts()
                {
                    this.MethodName = "embedded.token.getByZts";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("zts10esqvg76658fhzg9t5ctd3",
                    "Zenon.Tests.TestData.Api.Embedded.Token.GetByZts.json")]
                public async Task SingleResponseAsync(string tokenStandard, string resourceName)
                {
                    // Setup
                    var zts = TokenStandard.Parse(tokenStandard);
                    var api = new TokenApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, zts.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetByZts(zts);

                    // Validate
                    result.Should().NotBeNull();
                }
            }
        }
        #endregion

        #region Ledger
        public class Ledger
        {
            public class GetUnconfirmedBlocksByAddress
            {
                public GetUnconfirmedBlocksByAddress()
                {
                    this.MethodName = "ledger.getUnconfirmedBlocksByAddress";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qqylpfyyrj5t8pe99n4nf4x634vxse96fg7sge", 0, Constants.MemoryPoolPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetUnconfirmedBlocksByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qqylpfyyrj5t8pe99n4nf4x634vxse96fg7sge", 0, Constants.MemoryPoolPageSize,
                    "Zenon.Tests.TestData.Api.Ledger.GetUnconfirmedBlocksByAddress.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetUnconfirmedBlocksByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetUnreceivedBlocksByAddress
            {
                public GetUnreceivedBlocksByAddress()
                {
                    this.MethodName = "ledger.getUnreceivedBlocksByAddress";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qq6g4jptwwjmp0ym38n0juru2zzygap6r42pc0", 0, Constants.MemoryPoolPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetUnreceivedBlocksByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qq6g4jptwwjmp0ym38n0juru2zzygap6r42pc0", 0, Constants.MemoryPoolPageSize,
                    "Zenon.Tests.TestData.Api.Ledger.GetUnreceivedBlocksByAddress.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetUnreceivedBlocksByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetFrontierAccountBlock
            {
                public GetFrontierAccountBlock()
                {
                    this.MethodName = "ledger.getFrontierAccountBlock";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y",
                    "Zenon.Tests.TestData.Api.Ledger.GetFrontierAccountBlock.json")]
                public async Task SingleResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetFrontierAccountBlock(addr);

                    // Validate
                    result.Should().NotBeNull();
                    result.Address.Should().Be(addr);
                }
            }

            public class GetAccountBlockByHash
            {
                public GetAccountBlockByHash()
                {
                    this.MethodName = "ledger.getAccountBlockByHash";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("7bb60617d15329480db329c1f32066c45e3b1e767d171502494ae82e35e69409",
                    "Zenon.Tests.TestData.Api.Ledger.GetAccountBlockByHash.json")]
                public async Task SingleResponseAsync(string hash, string resourceName)
                {
                    // Setup
                    var h = Hash.Parse(hash);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, h.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAccountBlockByHash(h);

                    // Validate
                    result.Should().NotBeNull();
                    result.Hash.Should().Be(h);
                }
            }

            public class GetAccountBlocksByHeight
            {
                public GetAccountBlocksByHeight()
                {
                    this.MethodName = "ledger.getAccountBlocksByHeight";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 500, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAccountBlocksByHeight(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 500, Constants.MemoryPoolPageSize,
                    "Zenon.Tests.TestData.Api.Ledger.GetAccountBlocksByHeight.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAccountBlocksByHeight(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetAccountBlocksByPage
            {
                public GetAccountBlocksByPage()
                {
                    this.MethodName = "ledger.getAccountBlocksByPage";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 0, 10)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAccountBlocksByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qqwttth8sj5fchuqyr0ctum63hax2rqfyswk8y", 0, 10,
                    "Zenon.Tests.TestData.Api.Ledger.GetAccountBlocksByPage.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAccountBlocksByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetFrontierMomentum
            {
                public GetFrontierMomentum()
                {
                    this.MethodName = "ledger.getFrontierMomentum";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("Zenon.Tests.TestData.Api.Ledger.GetFrontierMomentum.json")]
                public async Task SingleResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetFrontierMomentum();

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetMomentumBeforeTime
            {
                public GetMomentumBeforeTime()
                {
                    this.MethodName = "ledger.getMomentumBeforeTime";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData(0)]
                public async Task NullResponseAsync(long time)
                {
                    // Setup
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, time)
                        .WithNullResponse()));

                    // Execute
                    var result = await api.GetMomentumBeforeTime(time);

                    // Validate
                    result.Should().BeNull();
                }

                [Theory]
                [InlineData(1652798090, "Zenon.Tests.TestData.Api.Ledger.GetMomentumBeforeTime.json")]
                public async Task SingleResponseAsync(long time, string resourceName)
                {
                    // Setup
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, time)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetMomentumBeforeTime(time);

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetMomentumByHash
            {
                public GetMomentumByHash()
                {
                    this.MethodName = "ledger.getMomentumByHash";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData("230506e0ec25306a70aa5b685e68dc791b4eb7c41202b9eb813336f21d54a637")]
                public async Task NullResponseAsync(string hash)
                {
                    // Setup
                    var h = Hash.Parse(hash);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, h.ToString())
                        .WithNullResponse()));

                    // Execute
                    var result = await api.GetMomentumByHash(h);

                    // Validate
                    result.Should().BeNull();
                }

                [Theory]
                [InlineData("230506e0ec25306a70aa5b685e68dc791b4eb7c41202b9eb813336f21d54a637", "Zenon.Tests.TestData.Api.Ledger.GetMomentumByHash.json")]
                public async Task SingleResponseAsync(string hash, string resourceName)
                {
                    // Setup
                    var h = Hash.Parse(hash);
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, h.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetMomentumByHash(h);

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetMomentumsByHeight
            {
                public GetMomentumsByHeight()
                {
                    this.MethodName = "ledger.getMomentumsByHeight";
                }

                public string MethodName { get; set; }

                [Theory]
                [InlineData(140, 10)]
                public async Task EmptyResponseAsync(long height, long count)
                {
                    // Setup
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, height, count)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetMomentumsByHeight(height, count);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(140, 10,
                    "Zenon.Tests.TestData.Api.Ledger.GetMomentumsByHeight.json")]
                public async Task ListResponseAsync(long height, long count, string resourceName)
                {
                    // Setup
                    var api = new LedgerApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, height, count)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetMomentumsByHeight(height, count);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BePositive();
                    result.List.Should().NotBeEmpty();
                }
            }
        }
        #endregion
    }
}