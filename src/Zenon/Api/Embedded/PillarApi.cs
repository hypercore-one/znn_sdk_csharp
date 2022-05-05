using System.Linq;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class PillarApi
    {
        public PillarApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<long> GetDepositedQsr(Address address)
        {
            return await Client.SendRequest<long>("embedded.pillar.getDepositedQsr", address.ToString());
        }

        public async Task<UncollectedReward> getUncollectedReward(Address address)
        {
            var response = await Client.SendRequest<JUncollectedReward>("embedded.pillar.getUncollectedReward", address.ToString());
            return new UncollectedReward(response);
        }

        public async Task<RewardHistoryList> GetFrontierRewardByPage(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JRewardHistoryList>("embedded.pillar.getFrontierRewardByPage", address.ToString(), pageIndex, pageSize);
            return new RewardHistoryList(response);
        }

        public async Task<long> GetQsrRegistrationCost()
        {
            return await Client.SendRequest<long>("embedded.pillar.getQsrRegistrationCost");
        }

        public async Task<PillarInfoList> GetAll(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JPillarInfoList>("embedded.pillar.getAll", pageIndex, pageSize);
            return new PillarInfoList(response);
        }

        public async Task<PillarInfo[]> GetByOwner(Address address)
        {
            var response = await Client.SendRequest<JPillarInfo[]>("embedded.pillar.getByOwner", address.ToString());
            return response.Select(x => new PillarInfo(x)).ToArray();
        }


        public async Task<PillarInfo> GetByName(string name)
        {
            var response = await Client.SendRequest<JPillarInfo>("embedded.pillar.getByName", name);
            return response != null ? new PillarInfo(response) : null;
        }

        public async Task<bool> CheckNameAvailability(string name)
        {
            return await Client.SendRequest<bool>("embedded.pillar.checkNameAvailability", name);
        }

        public async Task<DelegationInfo> GetDelegatedPillar(Address address)
        {
            var response = await Client.SendRequest<JDelegationInfo>("embedded.pillar.getDelegatedPillar", address.ToString());
            return response != null ? new DelegationInfo(response) : null;
        }

        public async Task<PillarEpochHistoryList> getPillarEpochHistory(string name, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JPillarEpochHistoryList>("embedded.pillar.getPillarEpochHistory", name, pageIndex, pageSize);
            return new PillarEpochHistoryList(response);
        }

        public async Task<PillarEpochHistoryList> GetPillarsHistoryByEpoch(int epoch, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JPillarEpochHistoryList>("embedded.pillar.getPillarsHistoryByEpoch", epoch, pageIndex, pageSize);
            return new PillarEpochHistoryList(response);
        }
    }
}
