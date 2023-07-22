using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class BridgeApi
    {
        public BridgeApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<BridgeInfo> GetBridgeInfo()
        {
            var response = await Client.SendRequestAsync<JBridgeInfo>("embedded.bridge.getBridgeInfo");
            return new BridgeInfo(response);
        }

        public async Task<OrchestratorInfo> GetOrchestratorInfo()
        {
            var response = await Client.SendRequestAsync<JOrchestratorInfo>("embedded.bridge.getOrchestratorInfo");
            return new OrchestratorInfo(response);
        }

        public async Task<TimeChallengesList> GetTimeChallengesInfo()
        {
            var response = await Client.SendRequestAsync<JTimeChallengesList>("embedded.bridge.getTimeChallengesInfo");
            return new TimeChallengesList(response);
        }

        public async Task<SecurityInfo> GetSecurityInfo()
        {
            var response = await Client.SendRequestAsync<JSecurityInfo>("embedded.bridge.getSecurityInfo");
            return new SecurityInfo(response);
        }

        public async Task<BridgeNetworkInfo> GetNetworkInfo(int networkClass, int chainId)
        {
            var response = await Client.SendRequestAsync<JBridgeNetworkInfo>("embedded.bridge.getNetworkInfo", networkClass, chainId);
            return new BridgeNetworkInfo(response);
        }

        public async Task<WrapTokenRequest> GetWrapTokenRequestById(Hash id)
        {
            var response = await Client.SendRequestAsync<JWrapTokenRequest>("embedded.bridge.getWrapTokenRequestById", id.ToString());
            return new WrapTokenRequest(response);
        }

        public async Task<WrapTokenRequestList> GetAllWrapTokenRequests(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JWrapTokenRequestList>("embedded.bridge.getAllWrapTokenRequests", pageIndex, pageSize);
            return new WrapTokenRequestList(response);
        }

        public async Task<WrapTokenRequestList> GetAllWrapTokenRequestsByToAddress(string toAddress, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JWrapTokenRequestList>("embedded.bridge.getAllWrapTokenRequestsByToAddress", toAddress, pageIndex, pageSize);
            return new WrapTokenRequestList(response);
        }

        public async Task<WrapTokenRequestList> GetAllWrapTokenRequestsByToAddressNetworkClassAndChainId(string toAddress, int networkClass, int chainId, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JWrapTokenRequestList>("embedded.bridge.getAllWrapTokenRequestsByToAddressNetworkClassAndChainId", toAddress, networkClass, chainId, pageIndex, pageSize);
            return new WrapTokenRequestList(response);
        }

        public async Task<BridgeNetworkInfoList> GetAllNetworks(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JBridgeNetworkInfoList>("embedded.bridge.getAllNetworks", pageIndex, pageSize);
            return new BridgeNetworkInfoList(response);
        }

        public async Task<WrapTokenRequestList> GetAllUnsignedWrapTokenRequests(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JWrapTokenRequestList>("embedded.bridge.getAllUnsignedWrapTokenRequests", pageIndex, pageSize);
            return new WrapTokenRequestList(response);
        }

        public async Task<UnwrapTokenRequest> GetUnwrapTokenRequestByHashAndLog(Hash hash, int logIndex)
        {
            var response = await Client.SendRequestAsync<JUnwrapTokenRequest>("embedded.bridge.getUnwrapTokenRequestByHashAndLog", hash.ToString(), logIndex);
            return new UnwrapTokenRequest(response);
        }

        public async Task<UnwrapTokenRequestList> GetAllUnwrapTokenRequests(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JUnwrapTokenRequestList>("embedded.bridge.getAllUnwrapTokenRequests", pageIndex, pageSize);
            return new UnwrapTokenRequestList(response);
        }

        public async Task<UnwrapTokenRequestList> GetAllUnwrapTokenRequestsByToAddress(string toAddress, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JUnwrapTokenRequestList>("embedded.bridge.getAllUnwrapTokenRequestsByToAddress", toAddress, pageIndex, pageSize);
            return new UnwrapTokenRequestList(response);
        }

        // Contract methods

        public AccountBlockTemplate WrapToken(int networkClass, int chainId, string toAddress, long amount, TokenStandard tokenStandard)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, tokenStandard, amount,
                Definitions.Bridge.EncodeFunction(nameof(WrapToken),
                    networkClass, chainId, toAddress));
        }

        public AccountBlockTemplate UpdateWrapRequest(Hash id, string signature)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(UpdateWrapRequest),
                    id, signature));
        }

        public AccountBlockTemplate UnwrapToken(int networkClass, int chainId, string tokenAddress, Hash txHash, int logIndex, long amount, Address toAddress, string signature)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(UnwrapToken),
                    networkClass,
                    chainId,
                    txHash,
                    logIndex,
                    toAddress,
                    tokenAddress,
                    amount,
                    signature));
        }

        public AccountBlockTemplate Redeem(Hash hash, int logIndex)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(Redeem),
                    hash.ToString(),
                    logIndex));
        }

        public AccountBlockTemplate Halt(string signature)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(Halt),
                    signature));
        }

        public AccountBlockTemplate Unhalt()
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(Unhalt)));
        }

        public AccountBlockTemplate SetAllowKeyGen(bool allowKeyGen)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(SetAllowKeyGen),
                    allowKeyGen));
        }

        public AccountBlockTemplate ChangeTssECDSAPubKey(string pubKey, string signature, string newSignature)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(ChangeTssECDSAPubKey),
                    pubKey,
                    signature,
                    newSignature));
        }

        public AccountBlockTemplate ChangeAdministrator(Address administrator)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(ChangeAdministrator),
                    administrator.ToString()));
        }

        public AccountBlockTemplate SetNetwork(int networkClass, int chainId, string name, string contractAddress, string metadata)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(SetNetwork),
                    networkClass,
                    chainId,
                    name,
                    contractAddress,
                    metadata));
        }

        public AccountBlockTemplate RemoveNetwork(int networkClass, int chainId)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(RemoveNetwork),
                    networkClass,
                    chainId));
        }

        public AccountBlockTemplate SetTokenPair(int networkClass, int chainId, TokenStandard tokenStandard, string tokenAddress, bool bridgeable, bool redeemable, bool owned, long minAmount, int fee, int redeemDelay, string metadata)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(SetTokenPair),
                    networkClass,
                    chainId,
                    tokenStandard,
                    tokenAddress,
                    bridgeable,
                    redeemable,
                    owned,
                    minAmount,
                    fee,
                    redeemDelay,
                    metadata));
        }

        public AccountBlockTemplate RemoveTokenPair(int networkClass, int chainId, TokenStandard tokenStandard, string tokenAddress)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(RemoveTokenPair),
                    networkClass,
                    chainId,
                    tokenStandard.ToString(),
                    tokenAddress));
        }

        public AccountBlockTemplate SetNetworkMetadata(int networkClass, int chainId, string metadata)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(SetNetworkMetadata),
                    networkClass,
                    chainId,
                    metadata));
        }

        public AccountBlockTemplate SetOrchestratorInfo(long windowSize, int keyGenThreshold, int confirmationsToFinality, int estimatedMomentumTime)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(SetOrchestratorInfo),
                    windowSize,
                    keyGenThreshold,
                    confirmationsToFinality,
                    estimatedMomentumTime));
        }

        public AccountBlockTemplate NominateGuardians(Address[] guardians)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(NominateGuardians),
                    new object[] { guardians }));
        }

        public AccountBlockTemplate SetBridgeMetadata(string metadata)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(SetBridgeMetadata),
                    metadata));
        }

        public AccountBlockTemplate ProposeAdministrator(Address administrator)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(ProposeAdministrator),
                    administrator));
        }

        public AccountBlockTemplate Emergency()
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(Emergency)));
        }

        public AccountBlockTemplate SetRedeemDelay(long redeemDelay)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(SetRedeemDelay),
                    redeemDelay));
        }

        public AccountBlockTemplate RevokeUnwrapRequest(Hash transactionHash, int logIndex)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.BridgeAddress, TokenStandard.ZnnZts, 0,
                Definitions.Bridge.EncodeFunction(nameof(RevokeUnwrapRequest),
                    transactionHash,
                    logIndex));
        }
    }
}