using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;
using Zenon.Api.Embedded;
using Zenon.Client;
using Zenon.Model.Embedded;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Api
{
    public partial class ApiTest
    {
        #region Embedded

        public class Bridge
        {
            public class GetBridgeInfo
            {
                public GetBridgeInfo()
                {
                    this.MethodName = "embedded.bridge.getBridgeInfo";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("Zenon.Resources.api.embedded.bridge.getBridgeInfo.json")]
                public async Task SingleResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetBridgeInfo();

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetOrchestratorInfo
            {
                public GetOrchestratorInfo()
                {
                    this.MethodName = "embedded.bridge.getOrchestratorInfo";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("Zenon.Resources.api.embedded.bridge.getOrchestratorInfo.json")]
                public async Task SingleResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetOrchestratorInfo();

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetTimeChallengesInfo
            {
                public GetTimeChallengesInfo()
                {
                    this.MethodName = "embedded.bridge.getTimeChallengesInfo";
                }

                public string MethodName { get; }

                [Fact]
                public async Task EmptyResponseAsync()
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetTimeChallengesInfo();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("Zenon.Resources.api.embedded.bridge.getTimeChallengesInfo.json")]
                public async Task ListResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetTimeChallengesInfo();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetSecurityInfo
            {
                public GetSecurityInfo()
                {
                    this.MethodName = "embedded.bridge.getSecurityInfo";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("Zenon.Resources.api.embedded.bridge.getSecurityInfo.json")]
                public async Task SingleResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetSecurityInfo();

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetNetworkInfo
            {
                public GetNetworkInfo()
                {
                    this.MethodName = "embedded.bridge.getNetworkInfo";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(2, 123, "Zenon.Resources.api.embedded.bridge.getNetworkInfo.json")]
                public async Task SingleResponseAsync(int networkClass, int chainId, string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, networkClass, chainId)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetNetworkInfo(networkClass, chainId);

                    // Validate
                    result.Should().NotBeNull();
                    result.NetworkClass.Should().Be(networkClass);
                    result.ChainId.Should().Be(chainId);
                }
            }

            public class GetWrapTokenRequestById
            {
                public GetWrapTokenRequestById()
                {
                    this.MethodName = "embedded.bridge.getWrapTokenRequestById";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("11ac76e40cc23674300f68ca87f5ebeb7210fc327fd43f35081b75a839c9c632", "Zenon.Resources.api.embedded.bridge.getWrapTokenRequestById.json")]
                public async Task SingleResponseAsync(string hash, string resourceName)
                {
                    // Setup
                    var h = Hash.Parse(hash);
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, h.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetWrapTokenRequestById(h);

                    // Validate
                    result.Should().NotBeNull();
                    result.Id.Should().Be(h);
                }
            }

            public class GetAllWrapTokenRequests
            {
                public GetAllWrapTokenRequests()
                {
                    this.MethodName = "embedded.bridge.getAllWrapTokenRequests";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAllWrapTokenRequests(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.bridge.getAllWrapTokenRequests.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAllWrapTokenRequests(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetAllWrapTokenRequestsByToAddress
            {
                public GetAllWrapTokenRequestsByToAddress()
                {
                    this.MethodName = "embedded.bridge.getAllWrapTokenRequestsByToAddress";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("0xb794f5ea0ba39494ce839613fffba74279579268", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string toAddress, int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, toAddress, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAllWrapTokenRequestsByToAddress(toAddress, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("0xb794f5ea0ba39494ce839613fffba74279579268", 0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.bridge.getAllWrapTokenRequestsByToAddress.json")]
                public async Task ListResponseAsync(string toAddress, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, toAddress, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAllWrapTokenRequestsByToAddress(toAddress, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetAllWrapTokenRequestsByToAddressNetworkClassAndChainId
            {
                public GetAllWrapTokenRequestsByToAddressNetworkClassAndChainId()
                {
                    this.MethodName = "embedded.bridge.getAllWrapTokenRequestsByToAddressNetworkClassAndChainId";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("0xb794f5ea0ba39494ce839613fffba74279579268", 2, 123, 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string toAddress, int networkClass, int chainId, int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, toAddress, networkClass, chainId, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAllWrapTokenRequestsByToAddressNetworkClassAndChainId(toAddress, networkClass, chainId, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("0xb794f5ea0ba39494ce839613fffba74279579268", 2, 123, 0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.bridge.getAllWrapTokenRequestsByToAddressNetworkClassAndChainId.json")]
                public async Task ListResponseAsync(string toAddress, int networkClass, int chainId, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, toAddress, networkClass, chainId, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAllWrapTokenRequestsByToAddressNetworkClassAndChainId(toAddress, networkClass, chainId, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetAllNetworks
            {
                public GetAllNetworks()
                {
                    this.MethodName = "embedded.bridge.getAllNetworks";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAllNetworks(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.bridge.getAllNetworks.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAllNetworks(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetAllUnsignedWrapTokenRequests
            {
                public GetAllUnsignedWrapTokenRequests()
                {
                    this.MethodName = "embedded.bridge.getAllUnsignedWrapTokenRequests";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAllUnsignedWrapTokenRequests(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.bridge.getAllUnsignedWrapTokenRequests.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAllUnsignedWrapTokenRequests(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetUnwrapTokenRequestByHashAndLog
            {
                public GetUnwrapTokenRequestByHashAndLog()
                {
                    this.MethodName = "embedded.bridge.getUnwrapTokenRequestByHashAndLog";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("11ac76e40cc23674300f68ca87f5ebeb7210fc327fd43f35081b75a839c9c632", 200, "Zenon.Resources.api.embedded.bridge.getUnwrapTokenRequestByHashAndLog.json")]
                public async Task SingleResponseAsync(string hash, int logIndex, string resourceName)
                {
                    // Setup
                    var h = Hash.Parse(hash);
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, h.ToString(), logIndex)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetUnwrapTokenRequestByHashAndLog(h, logIndex);

                    // Validate
                    result.Should().NotBeNull();
                    result.TransactionHash.Should().Be(h);
                    result.LogIndex.Should().Be(logIndex);
                }
            }

            public class GetAllUnwrapTokenRequests
            {
                public GetAllUnwrapTokenRequests()
                {
                    this.MethodName = "embedded.bridge.getAllUnwrapTokenRequests";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAllUnwrapTokenRequests(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.bridge.getAllUnwrapTokenRequests.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAllUnwrapTokenRequests(pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetAllUnwrapTokenRequestsByToAddress
            {
                public GetAllUnwrapTokenRequestsByToAddress()
                {
                    this.MethodName = "embedded.bridge.getAllUnwrapTokenRequestsByToAddress";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("0xb794f5ea0ba39494ce839613fffba74279579268", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string toAddress, int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, toAddress, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAllUnwrapTokenRequestsByToAddress(toAddress, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("0xb794f5ea0ba39494ce839613fffba74279579268", 0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.bridge.getAllUnwrapTokenRequestsByToAddress.json")]
                public async Task ListResponseAsync(string toAddress, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new BridgeApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, toAddress, pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetAllUnwrapTokenRequestsByToAddress(toAddress, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }
        }

        public class Liquidity
        {
            public class GetUncollectedReward
            {
                public GetUncollectedReward()
                {
                    this.MethodName = "embedded.liquidity.getUncollectedReward";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qzal6c5s9rjnnxd2z7dvdhjxpmmj4fmw56a0mz",
                    "Zenon.Resources.api.embedded.liquidity.getUncollectedReward.json")]
                public async Task SingleResponseAsync(string address, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
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
                    this.MethodName = "embedded.liquidity.getFrontierRewardByPage";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqjnwjjpnue8xmmpanz6csze6tcmtzzdtfsww7", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
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
                [InlineData("z1qqjnwjjpnue8xmmpanz6csze6tcmtzzdtfsww7", 0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.liquidity.getFrontierRewardByPage.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetFrontierRewardByPage(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetLiquidityInfo
            {
                public GetLiquidityInfo()
                {
                    this.MethodName = "embedded.liquidity.getLiquidityInfo";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("Zenon.Resources.api.embedded.liquidity.getLiquidityInfo.json")]
                public async Task SingleResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetLiquidityInfo();

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetSecurityInfo
            {
                public GetSecurityInfo()
                {
                    this.MethodName = "embedded.liquidity.getSecurityInfo";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("Zenon.Resources.api.embedded.liquidity.getSecurityInfo.json")]
                public async Task SingleResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetSecurityInfo();

                    // Validate
                    result.Should().NotBeNull();
                }
            }

            public class GetLiquidityStakeEntriesByAddress
            {
                public GetLiquidityStakeEntriesByAddress()
                {
                    this.MethodName = "embedded.liquidity.getLiquidityStakeEntriesByAddress";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqjnwjjpnue8xmmpanz6csze6tcmtzzdtfsww7", 0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(string address, int pageIndex, int pageSize)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetLiquidityStakeEntriesByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("z1qqjnwjjpnue8xmmpanz6csze6tcmtzzdtfsww7", 0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.liquidity.getLiquidityStakeEntriesByAddress.json")]
                public async Task ListResponseAsync(string address, int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString(), pageIndex, pageSize)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetLiquidityStakeEntriesByAddress(addr, pageIndex, pageSize);

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }

            public class GetTimeChallengesInfo
            {
                public GetTimeChallengesInfo()
                {
                    this.MethodName = "embedded.liquidity.getTimeChallengesInfo";
                }

                public string MethodName { get; }

                [Fact]
                public async Task EmptyResponseAsync()
                {
                    // Setup
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetTimeChallengesInfo();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData("Zenon.Resources.api.embedded.liquidity.getTimeChallengesInfo.json")]
                public async Task ListResponseAsync(string resourceName)
                {
                    // Setup
                    var api = new LiquidityApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName)
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetTimeChallengesInfo();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().BeGreaterThan(0);
                    result.List.Should().NotBeEmpty();
                }
            }
        }

        public class Htlc
        {
            public class GetById
            {
                public GetById()
                {
                    this.MethodName = "embedded.htlc.getById";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("11ac76e40cc23674300f68ca87f5ebeb7210fc327fd43f35081b75a839c9c632",
                    "Zenon.Resources.api.embedded.htlc.getById.json")]
                public async Task SingleResponseAsync(string id, string resourceName)
                {
                    // Setup
                    var hash = Hash.Parse(id);
                    var api = new HtlcApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, hash.ToString())
                        .WithManifestResourceTextResponse(resourceName)));

                    // Execute
                    var result = await api.GetById(hash);

                    // Validate
                    result.Should().NotBeNull();
                    result.Id.Should().Be(hash);
                }
            }

            public class GetProxyUnlockStatus
            {
                public GetProxyUnlockStatus()
                {
                    this.MethodName = "embedded.htlc.getProxyUnlockStatus";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData("z1qqjnwjjpnue8xmmpanz6csze6tcmtzzdtfsww7", "true", true)]
                [InlineData("z1qpsjv3wzzuuzdudg7tf6uhvr6sk4ag8me42ua4", "false", false)]
                public async Task SingleResponseAsync(string address, string response, bool expectedResult)
                {
                    // Setup
                    var addr = Address.Parse(address);
                    var api = new HtlcApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, addr.ToString())
                        .WithResponse(() => response)));

                    // Execute
                    var result = await api.GetProxyUnlockStatus(addr);

                    // Validate
                    result.Should().Be(expectedResult);
                }
            }
        }
        
        public class Spork
        {
            public class GetAll
            {
                public GetAll()
                {
                    this.MethodName = "embedded.spork.getAll";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new SporkApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAll();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.spork.getAll.json")]
                public async Task ListResponseAsync(int pageIndex, int pageSize, string resourceName)
                {
                    // Setup
                    var api = new SporkApi(new Lazy<IClient>(() => new TestClient()
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
        }
        
        public class Accelerator
        {
            public class GetAll
            {
                public GetAll()
                {
                    this.MethodName = "embedded.accelerator.getAll";
                }

                public string MethodName { get; }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize)]
                public async Task EmptyResponseAsync(int pageIndex, int pageSize)
                {
                    // Setup
                    var api = new AcceleratorApi(new Lazy<IClient>(() => new TestClient()
                        .WithMethod(this.MethodName, pageIndex, pageSize)
                        .WithEmptyResponse()));

                    // Execute
                    var result = await api.GetAll();

                    // Validate
                    result.Should().NotBeNull();
                    result.Count.Should().Be(0);
                    result.List.Should().BeEmpty();
                }

                [Theory]
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.accelerator.getAll.json")]
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
                    "Zenon.Resources.api.embedded.accelerator.getProjectById.json")]
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
                    "Zenon.Resources.api.embedded.accelerator.getPhaseById.json")]
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
                    "Zenon.Resources.api.embedded.accelerator.getPillarVotes.json")]
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
                    "Zenon.Resources.api.embedded.accelerator.getVoteBreakdown.json")]
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
                    "Zenon.Resources.api.embedded.pillar.getUncollectedReward.json")]
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
                    "Zenon.Resources.api.embedded.pillar.getFrontierRewardByPage.json")]
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
                    "Zenon.Resources.api.embedded.pillar.getAll.json")]
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
                    "Zenon.Resources.api.embedded.pillar.getByOwner.json")]
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
                    "Zenon.Resources.api.embedded.pillar.getByName.json")]
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
                    "Zenon.Resources.api.embedded.pillar.getDelegatedPillar.json")]
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
                    "Zenon.Resources.api.embedded.pillar.getPillarEpochHistory.json")]
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
                    "Zenon.Resources.api.embedded.pillar.getPillarsHistoryByEpoch.json")]
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
                    "Zenon.Resources.api.embedded.plasma.get.json")]
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
                    "Zenon.Resources.api.embedded.plasma.getEntriesByAddress.json")]
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
                    new byte[0], "Zenon.Resources.api.embedded.plasma.getRequiredPoWForAccountBlock.json")]
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
                    "Zenon.Resources.api.embedded.sentinel.getAllActive.json")]
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
                    "Zenon.Resources.api.embedded.sentinel.getByOwner.json")]
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
                    "Zenon.Resources.api.embedded.sentinel.getUncollectedReward.json")]
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
                    "Zenon.Resources.api.embedded.sentinel.getFrontierRewardByPage.json")]
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
                    "Zenon.Resources.api.embedded.stake.getEntriesByAddress.json")]
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
                    "Zenon.Resources.api.embedded.stake.getFrontierRewardByPage.json")]
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
                    "Zenon.Resources.api.embedded.stake.getUncollectedReward.json")]
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
                [InlineData("Zenon.Resources.api.embedded.swap.getAssets.json")]
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
                    "Zenon.Resources.api.embedded.swap.getAssetsByKeyIdHash.json")]
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
                [InlineData("Zenon.Resources.api.embedded.swap.getLegacyPillars.json")]
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
                [InlineData(0, Constants.RpcMaxPageSize, "Zenon.Resources.api.embedded.token.getAll.json")]
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
                    "Zenon.Resources.api.embedded.token.getByOwner.json")]
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
                    "Zenon.Resources.api.embedded.token.getByZts.json")]
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
                    "Zenon.Resources.api.ledger.getUnconfirmedBlocksByAddress.json")]
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
                    "Zenon.Resources.api.ledger.getUnreceivedBlocksByAddress.json")]
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
                    "Zenon.Resources.api.ledger.getFrontierAccountBlock.json")]
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
                    "Zenon.Resources.api.ledger.getAccountBlockByHash.json")]
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
                    "Zenon.Resources.api.ledger.getAccountBlocksByHeight.json")]
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
                    "Zenon.Resources.api.ledger.getAccountBlocksByPage.json")]
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
                [InlineData("Zenon.Resources.api.ledger.getFrontierMomentum.json")]
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
                [InlineData(1652798090, "Zenon.Resources.api.ledger.getMomentumBeforeTime.json")]
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
                [InlineData("230506e0ec25306a70aa5b685e68dc791b4eb7c41202b9eb813336f21d54a637", "Zenon.Resources.api.ledger.getMomentumByHash.json")]
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
                    "Zenon.Resources.api.ledger.getMomentumsByHeight.json")]
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