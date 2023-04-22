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
        public static readonly Abi.Abi Htlc = new Abi.Abi(HtlcDefinition);
        public static readonly Abi.Abi Common = new Abi.Abi(CommonDefinition);
}
}
