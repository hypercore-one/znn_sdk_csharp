using System;
using System.Numerics;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class LiquidityApi
    {
        public LiquidityApi(Lazy<IClient> client)
        {
            Client = client;
        }

        public Lazy<IClient> Client { get; }

        public async Task<UncollectedReward> GetUncollectedReward(Address address)
        {
            var response = await Client.Value.SendRequest<JUncollectedReward>("embedded.liquidity.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JRewardHistoryList>("embedded.liquidity.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }

        public async Task<LiquidityInfo> GetLiquidityInfo()
        {
            var response = await Client.Value.SendRequest<JLiquidityInfo>("embedded.liquidity.getLiquidityInfo");
            return new LiquidityInfo(response);
        }

        public async Task<SecurityInfo> GetSecurityInfo()
        {
            var response = await Client.Value.SendRequest<JSecurityInfo>("embedded.liquidity.getSecurityInfo");
            return new SecurityInfo(response);
        }

        public async Task<LiquidityStakeList> GetLiquidityStakeEntriesByAddress(Address address, uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JLiquidityStakeList>("embedded.liquidity.getLiquidityStakeEntriesByAddress", address.ToString(), pageIndex, pageSize);
            return new LiquidityStakeList(response);
        }

        public async Task<TimeChallengesList> GetTimeChallengesInfo()
        {
            var response = await Client.Value.SendRequest<JTimeChallengesList>("embedded.liquidity.getTimeChallengesInfo");
            return new TimeChallengesList(response);
        }

        // Contract methods

        /// <summary>
        /// Method for staking the liquidity for the Orbital Program.
        /// </summary>
        public AccountBlockTemplate LiquidityStake(long durationInSec, BigInteger amount, TokenStandard zts)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, zts, amount,
                Definitions.Liquidity.EncodeFunction(nameof(LiquidityStake),
                    durationInSec));
        }

        /// <summary>
        /// Method for cancelling the liquidity stake for the Orbital Program.
        /// </summary>
        public AccountBlockTemplate CancelLiquidityStake(Hash id)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(CancelLiquidityStake), id));
        }

        /// <summary>
        /// Method for unlocking the liquidity staking entries for a ZTS that is no longer allowed for staking.
        /// </summary>
        public AccountBlockTemplate UnlockLiquidityStakeEntries(TokenStandard zts)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, zts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(UnlockLiquidityStakeEntries)));
        }

        public AccountBlockTemplate BurnZnn(BigInteger burnAmount)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(BurnZnn), burnAmount));
        }

        public AccountBlockTemplate Fund(BigInteger znnReward, BigInteger qsrReward)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(Fund), znnReward, qsrReward));
        }

        public AccountBlockTemplate Donate()
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(Donate)));
        }

        public AccountBlockTemplate Update()
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(Update)));
        }

        // Contract methods for administrator only

        /// <summary>
        /// Method for putting the liquidity embedded contract into emergency mode.
        /// </summary>
        /// <remarks>
        /// Can only be called by the Administrator.
        /// </remarks>
        public AccountBlockTemplate Emergency()
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(Emergency)));
        }

        /// <summary>
        /// Method for nominating the guardians for the liquidity embedded contract. 
        /// </summary>
        /// <remarks>
        /// Can only be called by the Administrator. Guarded by a time challenge.
        /// </remarks>
        public AccountBlockTemplate NominateGuardians(Address[] guardians)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(NominateGuardians), new object[] { guardians }));
        }

        /// <summary>
        /// Method for changing the administrator for the liquidity embedded contract.
        /// </summary>
        /// <remarks>
        /// Can only be called by the administrator.
        /// </remarks>
        public AccountBlockTemplate ChangeAdministrator(Address administrator)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(ChangeAdministrator),
                    administrator));
        }

        /// <summary>
        /// Method for proposing a new administrator for the liquidity embedded contract.
        /// </summary>
        /// <remarks>
        /// Can only be called by the Administrator.
        /// </remarks>
        public AccountBlockTemplate ProposeAdministrator(Address administrator)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(ProposeAdministrator),
                    administrator));
        }

        /// <summary>
        /// Method for setting an additional reward for the liquidity staking.
        /// </summary>
        /// <remarks>
        /// Can only be called by the administrator.
        /// </remarks>
        public AccountBlockTemplate SetAdditionalReward(BigInteger znnAmount, BigInteger qsrAmount)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(SetAdditionalReward), znnAmount, qsrAmount));
        }

        /// <summary>
        /// Method for halting or unhalting the liquidity staking.
        /// </summary>
        /// <remarks>
        /// Can only be called by the administrator.
        /// </remarks>
        public AccountBlockTemplate SetIsHalted(bool value)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(SetIsHalted),
                    value));
        }

        /// <summary>
        /// Method for setting a token tuple and their corresponding reward percentages for the Orbital Program.
        /// </summary>
        /// <remarks>
        /// Can only be called by the administrator.
        /// </remarks>
        public AccountBlockTemplate SetTokenTuple(string[] tokenStandards, int[] znnPercentages, int[] qsrPercentages, BigInteger[] minAmounts)
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Liquidity.EncodeFunction(nameof(SetTokenTuple),
                    tokenStandards,
                    znnPercentages,
                    qsrPercentages,
                    minAmounts));
        }

        // Common contract methods

        public AccountBlockTemplate CollectReward()
        {
            return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Common.EncodeFunction(nameof(CollectReward)));
        }
    }
}