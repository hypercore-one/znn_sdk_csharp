using Zenon.Abi.Json;

namespace Zenon.Embedded
{
    public class Definitions
    {
        private static readonly JEntry[] PlasmaDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "Fuse", inputs = new JParam[]
            {
                new JParam() { name = "address", type = "address" }
            } },
            new JEntry() { type = "function", name = "CancelFuse", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" }
            } }
        };

        private static readonly JEntry[] PillarDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "Register", inputs = new JParam[]
            {
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "producerAddress", type = "address" },
                new JParam() { name = "rewardAddress", type = "address" },
                new JParam() { name = "giveBlockRewardPercentage", type = "uint8" },
                new JParam() { name = "giveDelegateRewardPercentage", type = "uint8" }
            } },
            new JEntry() { type = "function", name = "RegisterLegacy", inputs = new JParam[]
            {
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "producerAddress", type = "address" },
                new JParam() { name = "rewardAddress", type = "address" },
                new JParam() { name = "giveBlockRewardPercentage", type = "uint8" },
                new JParam() { name = "giveDelegateRewardPercentage", type = "uint8" },
                new JParam() { name = "publicKey", type = "string" },
                new JParam() { name = "signature", type = "string" }
            } },
            new JEntry() { type = "function", name = "Revoke", inputs = new JParam[]
            {
                new JParam() { name = "name", type = "string" }
            } },
            new JEntry() { type = "function", name = "UpdatePillar", inputs = new JParam[]
            {
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "producerAddress", type = "address" },
                new JParam() { name = "rewardAddress", type = "address" },
                new JParam() { name = "giveBlockRewardPercentage", type = "uint8" },
                new JParam() { name = "giveDelegateRewardPercentage", type = "uint8" }
            } },
            new JEntry() { type = "function", name = "Delegate", inputs = new JParam[]
            {
                new JParam() { name = "name", type = "string" }
            } },
            new JEntry() { type = "function", name = "Undelegate", inputs = new JParam[0] }
        };

        private static readonly JEntry[] TokenDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "IssueToken", inputs = new JParam[]
            {
                new JParam() { name = "tokenName", type = "string" },
                new JParam() { name = "tokenSymbol", type = "string" },
                new JParam() { name = "tokenDomain", type = "string" },
                new JParam() { name = "totalSupply", type = "uint256" },
                new JParam() { name = "maxSupply", type = "uint256" },
                new JParam() { name = "decimals", type = "uint8" },
                new JParam() { name = "isMintable", type = "bool" },
                new JParam() { name = "isBurnable", type = "bool" },
                new JParam() { name = "isUtility", type = "bool" }
            } },
            new JEntry() { type = "function", name = "Mint", inputs = new JParam[]
            {
                new JParam() { name = "tokenStandard", type = "tokenStandard" },
                new JParam() { name = "amount", type = "uint256" },
                new JParam() { name = "receiveAddress", type = "address" }
            } },
            new JEntry() { type = "function", name = "Burn", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "UpdateToken", inputs = new JParam[]
            {
                new JParam() { name = "tokenStandard", type = "tokenStandard" },
                new JParam() { name = "owner", type = "address" },
                new JParam() { name = "isMintable", type = "bool" },
                new JParam() { name = "isBurnable", type = "bool" }
            } },
        };

        private static readonly JEntry[] SentinelDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "Register", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "Revoke", inputs = new JParam[0] }
        };

        private static readonly JEntry[] SwapDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "RetrieveAssets", inputs = new JParam[]
            {
                new JParam() { name = "publicKey", type = "string" },
                new JParam() { name = "signature", type = "string" }
            } }
        };

        private static readonly JEntry[] StakeDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "Stake", inputs = new JParam[]
            {
                new JParam() { name = "durationInSec", type = "int64" }
            } },
             new JEntry() { type = "function", name = "Cancel", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" }
            } }
        };

        private static readonly JEntry[] AcceleratorDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "CreateProject", inputs = new JParam[]
            {
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "description", type = "string" },
                new JParam() { name = "url", type = "string" },
                new JParam() { name = "znnFundsNeeded", type = "uint256" },
                new JParam() { name = "qsrFundsNeeded", type = "uint256" }
            } },
            new JEntry() { type = "function", name = "AddPhase", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" },
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "description", type = "string" },
                new JParam() { name = "url", type = "string" },
                new JParam() { name = "znnFundsNeeded", type = "uint256" },
                new JParam() { name = "qsrFundsNeeded", type = "uint256" }
            } },
            new JEntry() { type = "function", name = "UpdatePhase", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" },
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "description", type = "string" },
                new JParam() { name = "url", type = "string" },
                new JParam() { name = "znnFundsNeeded", type = "uint256" },
                new JParam() { name = "qsrFundsNeeded", type = "uint256" }
            } },
            new JEntry() { type = "function", name = "Donate", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "VoteByName", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" },
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "vote", type = "uint8" }
            } },
            new JEntry() { type = "function", name = "VoteByProdAddress", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" },
                new JParam() { name = "vote", type = "uint8" }
            } },
        };

        private static readonly JEntry[] SporkDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "CreateSpork", inputs = new JParam[]
            {
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "description", type = "string" }
            } },
            new JEntry() { type = "function", name = "ActivateSpork", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" }
            } }
        };

        private static readonly JEntry[] HtlcDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "Create", inputs = new JParam[]
            {
                new JParam() { name = "hashLocked", type = "address" },
                new JParam() { name = "expirationTime", type = "int64" },
                new JParam() { name = "hashType", type = "uint8" },
                new JParam() { name = "keyMaxSize", type = "uint8" },
                new JParam() { name = "hashLock", type = "bytes" }
            } },
            new JEntry() { type = "function", name = "Reclaim", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" }
            } },
            new JEntry() { type = "function", name = "Unlock", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" },
                new JParam() { name = "preimage", type = "bytes" }
            } },
            new JEntry() { type = "function", name = "DenyProxyUnlock", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "AllowProxyUnlock", inputs = new JParam[0] }
        };

        private static readonly JEntry[] LiquidityDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "Update", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "Donate", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "Fund", inputs = new JParam[]
            {
                new JParam() { name = "znnReward", type = "uint256" },
                new JParam() { name = "qsrReward", type = "uint256" }
            } },
            new JEntry() { type = "function", name = "BurnZnn", inputs = new JParam[]
            {
                new JParam() { name = "burnAmount", type = "uint256" }
            } },
            new JEntry() { type = "function", name = "SetTokenTuple", inputs = new JParam[]
            {
                new JParam() { name = "tokenStandards", type = "string[]" },
                new JParam() { name = "znnPercentages", type = "uint32[]" },
                new JParam() { name = "qsrPercentages", type = "uint32[]" },
                new JParam() { name = "minAmounts", type = "uint256[]" }
            } },
            new JEntry() { type = "function", name = "NominateGuardians", inputs = new JParam[]
            {
                new JParam() { name = "guardians", type = "address[]" }
            } },
            new JEntry() { type = "function", name = "ProposeAdministrator", inputs = new JParam[]
            {
                new JParam() { name = "address", type = "address" }
            } },
            new JEntry() { type = "function", name = "Emergency", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "SetIsHalted", inputs = new JParam[]
            {
                new JParam() { name = "isHalted", type = "bool" }
            } },
            new JEntry() { type = "function", name = "LiquidityStake", inputs = new JParam[]
            {
                new JParam() { name = "durationInSec", type = "int64" }
            } },
            new JEntry() { type = "function", name = "CancelLiquidityStake", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" }
            } },
            new JEntry() { type = "function", name = "UnlockLiquidityStakeEntries", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "SetAdditionalReward", inputs = new JParam[]
            {
                new JParam() { name = "znnReward", type = "uint256" },
                new JParam() { name = "qsrReward", type = "uint256" }
            } },
            new JEntry() { type = "function", name = "ChangeAdministrator", inputs = new JParam[]
            {
                new JParam() { name = "administrator", type = "address" }
            } }
        };

        private static readonly JEntry[] BridgeDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "WrapToken", inputs = new JParam[]
            {
                new JParam() { name = "networkClass", type = "uint32" },
                new JParam() { name = "chainId", type = "uint32" },
                new JParam() { name = "toAddress", type = "string" }
            } },
            new JEntry() { type = "function", name = "UpdateWrapRequest", inputs = new JParam[]
            {
                new JParam() { name = "id", type = "hash" },
                new JParam() { name = "signature", type = "string" }
            } },
            new JEntry() { type = "function", name = "SetNetwork", inputs = new JParam[]
            {
                new JParam() { name = "networkClass", type = "uint32" },
                new JParam() { name = "chainId", type = "uint32" },
                new JParam() { name = "name", type = "string" },
                new JParam() { name = "contractAddress", type = "string" },
                new JParam() { name = "metadata", type = "string" }
            } },
            new JEntry() { type = "function", name = "RemoveNetwork", inputs = new JParam[]
            {
                new JParam() { name = "networkClass", type = "uint32" },
                new JParam() { name = "chainId", type = "uint32" }
            } },
            new JEntry() { type = "function", name = "SetTokenPair", inputs = new JParam[]
            {
                new JParam() { name = "networkClass", type = "uint32" },
                new JParam() { name = "chainId", type = "uint32" },
                new JParam() { name = "tokenStandard", type = "tokenStandard" },
                new JParam() { name = "tokenAddress", type = "string" },
                new JParam() { name = "bridgeable", type = "bool" },
                new JParam() { name = "redeemable", type = "bool" },
                new JParam() { name = "owned", type = "bool" },
                new JParam() { name = "minAmount", type = "uint256" },
                new JParam() { name = "feePercentage", type = "uint32" },
                new JParam() { name = "redeemDelay", type = "uint32" },
                new JParam() { name = "metadata", type = "string" },
            } },
            new JEntry() { type = "function", name = "SetNetworkMetadata", inputs = new JParam[]
            {
                new JParam() { name = "networkClass", type = "uint32" },
                new JParam() { name = "chainId", type = "uint32" },
                new JParam() { name = "metadata", type = "string" }
            } },
            new JEntry() { type = "function", name = "RemoveTokenPair", inputs = new JParam[]
            {
                new JParam() { name = "networkClass", type = "uint32" },
                new JParam() { name = "chainId", type = "uint32" },
                new JParam() { name = "tokenStandard", type = "tokenStandard" },
                new JParam() { name = "tokenAddress", type = "string" }
            } },
            new JEntry() { type = "function", name = "Halt", inputs = new JParam[]
            {
                new JParam() { name = "signature", type = "string" }
            } },
            new JEntry() { type = "function", name = "Unhalt", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "Emergency", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "ChangeTssECDSAPubKey", inputs = new JParam[]
            {
                new JParam() { name = "pubKey", type = "string" },
                new JParam() { name = "oldPubKeySignature", type = "string" },
                new JParam() { name = "newPubKeySignature", type = "string" }
            } },

            new JEntry() { type = "function", name = "ChangeAdministrator", inputs = new JParam[]
            {
                new JParam() { name = "administrator", type = "address" }
            } },
            new JEntry() { type = "function", name = "ProposeAdministrator", inputs = new JParam[]
            {
                new JParam() { name = "address", type = "address" }
            } },
            new JEntry() { type = "function", name = "SetAllowKeyGen", inputs = new JParam[]
            {
                new JParam() { name = "allowKeyGen", type = "bool" }
            } },
            new JEntry() { type = "function", name = "SetBridgeMetadata", inputs = new JParam[]
            {
                new JParam() { name = "metadata", type = "string" }
            } },
            new JEntry() { type = "function", name = "UnwrapToken", inputs = new JParam[]
            {
                new JParam() { name = "networkClass", type = "uint32" },
                new JParam() { name = "chainId", type = "uint32" },
                new JParam() { name = "transactionHash", type = "hash" },
                new JParam() { name = "logIndex", type = "uint32" },
                new JParam() { name = "toAddress", type = "address" },
                new JParam() { name = "tokenAddress", type = "string" },
                new JParam() { name = "amount", type = "uint256" },
                new JParam() { name = "signature", type = "string" }
            } },
            new JEntry() { type = "function", name = "RevokeUnwrapRequest", inputs = new JParam[]
            {
                new JParam() { name = "transactionHash", type = "hash" },
                new JParam() { name = "logIndex", type = "uint32" }
            } },
            new JEntry() { type = "function", name = "Redeem", inputs = new JParam[]
            {
                new JParam() { name = "transactionHash", type = "hash" },
                new JParam() { name = "logIndex", type = "uint32" }
            } },
            new JEntry() { type = "function", name = "NominateGuardians", inputs = new JParam[]
            {
                new JParam() { name = "guardians", type = "address[]" }
            } },
            new JEntry() { type = "function", name = "SetOrchestratorInfo", inputs = new JParam[]
            {
                new JParam() { name = "windowSize", type = "uint64" },
                new JParam() { name = "keyGenThreshold", type = "uint32" },
                new JParam() { name = "confirmationsToFinality", type = "uint32" },
                new JParam() { name = "estimatedMomentumTime", type = "uint32" }
            } }
        };

        // Common definitions of embedded methods
        private static readonly JEntry[] CommonDefinition = new JEntry[]
        {
            new JEntry() { type = "function", name = "DepositQsr", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "WithdrawQsr", inputs = new JParam[0] },
            new JEntry() { type = "function", name = "CollectReward", inputs = new JParam[0] },
        };

        // ABI definitions of embedded contracts
        public static readonly Abi.Abi Plasma = new Abi.Abi(PlasmaDefinition);
        public static readonly Abi.Abi Pillar = new Abi.Abi(PillarDefinition);
        public static readonly Abi.Abi Token = new Abi.Abi(TokenDefinition);
        public static readonly Abi.Abi Sentinel = new Abi.Abi(SentinelDefinition);
        public static readonly Abi.Abi Swap = new Abi.Abi(SwapDefinition);
        public static readonly Abi.Abi Stake = new Abi.Abi(StakeDefinition);
        public static readonly Abi.Abi Accelerator = new Abi.Abi(AcceleratorDefinition);
        public static readonly Abi.Abi Spork = new Abi.Abi(SporkDefinition);
        public static readonly Abi.Abi Htlc = new Abi.Abi(HtlcDefinition);
        public static readonly Abi.Abi Liquidity = new Abi.Abi(LiquidityDefinition);
        public static readonly Abi.Abi Bridge = new Abi.Abi(BridgeDefinition);
        public static readonly Abi.Abi Common = new Abi.Abi(CommonDefinition);
    }
}