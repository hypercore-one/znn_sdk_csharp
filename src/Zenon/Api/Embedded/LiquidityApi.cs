using System;
using System.Linq;
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

		public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
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

		public async Task<LiquidityStakeList> GetLiquidityStakeEntriesByAddress(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
		{
			var response = await Client.Value.SendRequest<JLiquidityStakeList>("embedded.liquidity.getLiquidityStakeEntriesByAddress", address.ToString(), pageIndex, pageSize);
			return new LiquidityStakeList(response);
		}

		public async Task<TimeChallengesList> GetTimeChallengesInfo(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
		{
			var response = await Client.Value.SendRequest<JTimeChallengesList>("embedded.liquidity.getTimeChallengesInfo");
			return new TimeChallengesList(response);
		}

		// Contract methods
		public AccountBlockTemplate SetTokenTuple(string[] tokenStandards, int[] znnPercentages, int[] qsrPercentages, long[] minAmounts)
		{
			return AccountBlockTemplate.CallContract(
				Address.LiquidityAddress,
				TokenStandard.ZnnZts,
				0,
				Definitions.Liquidity.EncodeFunction("SetTokenTuple",
					tokenStandards,
					znnPercentages,
					qsrPercentages,
					minAmounts));
		}

		public AccountBlockTemplate LiquidityStake(TokenStandard zts, long amount, long durationInSec)
		{
			return AccountBlockTemplate.CallContract(
				Address.LiquidityAddress,
				zts,
				amount,
				Definitions.Liquidity.EncodeFunction("LiquidityStake",
					durationInSec));
		}

		public AccountBlockTemplate SetIsHalted(bool value)
		{
			return AccountBlockTemplate.CallContract(
				Address.LiquidityAddress,
				TokenStandard.ZnnZts,
				0,
				Definitions.Liquidity.EncodeFunction("SetIsHalted",
					value));
		}

		public AccountBlockTemplate CollectReward()
		{
			return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, 0,
				Definitions.Liquidity.EncodeFunction("CollectReward"));
		}

		public AccountBlockTemplate CancelLiquidity(Hash id)
		{
			return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, 0,
				Definitions.Liquidity.EncodeFunction("CancelLiquidity", id));
		}

		public AccountBlockTemplate UnlockLiquidityStakeEntries(TokenStandard zts)
		{
			return AccountBlockTemplate.CallContract(Address.LiquidityAddress, zts, 0,
				Definitions.Common.EncodeFunction("UnlockLiquidityStakeEntries"));
		}

		public AccountBlockTemplate SetAdditionalReward(long znnAmount, long qsrAmount)
		{
			return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, 0,
				Definitions.Common.EncodeFunction("SetAdditionalReward", znnAmount, qsrAmount));
		}

		public AccountBlockTemplate NominateGuardians(Address[] guardians)
		{
			return AccountBlockTemplate.CallContract(Address.LiquidityAddress, TokenStandard.ZnnZts, 0,
				Definitions.Common.EncodeFunction("NominateGuardians", guardians.Select(x => x.ToString()).ToArray()));
		}
    }
}