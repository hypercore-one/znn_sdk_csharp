using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Api.Embedded
{
    public class AcceleratorApi
    {
        public AcceleratorApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<ProjectList> GetAll(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JProjectList>("embedded.accelerator.getAll", pageIndex, pageSize);
            return new ProjectList(response);
        }

        public async Task<Project> GetProjectById(string id)
        {
            var response = await Client.SendRequest<JProject>("embedded.accelerator.getProjectById", id);
            return new Project(response);
        }

        public async Task<Phase> GetPhaseById(Hash id)
        {
            var response = await Client.SendRequest<JObject>("embedded.accelerator.getPhaseById", id.ToString());
            return new Phase(JPhase.FromJObject(response));
        }

        public async Task<PillarVote[]> GetPillarVotes(string name, string[] hashes)
        {
            var response = await Client.SendRequest<JPillarVote[]>("embedded.accelerator.getPillarVotes", name, hashes);
            return response.Select(x => new PillarVote(x)).ToArray();
        }

        public async Task<VoteBreakdown> GetVoteBreakdown(Hash id)
        {
            var response = await Client.SendRequest<JVoteBreakdown>("embedded.accelerator.getVoteBreakdown", id.ToString());
            return new VoteBreakdown(response);
        }

        // Contract methods
        public AccountBlockTemplate CreateProject(string name, string description, string url, long znnFundsNeeded, long qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts,
                AmountUtils.ExtractDecimals(Constants.ProjectCreationFeeInZnn, Constants.ZnnDecimals),
                Definitions.Accelerator.EncodeFunction("CreateProject", name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate AddPhase(Hash id, string name, string description, string url, long znnFundsNeeded, long qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts, 0,
                Definitions.Accelerator.EncodeFunction("AddPhase", id.Bytes, name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate UpdatePhase(Hash id, string name, string description, string url, long znnFundsNeeded, long qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts, 0,
                Definitions.Accelerator.EncodeFunction("UpdatePhase", id.Bytes, name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate Donate(long amount, TokenStandard zts)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, zts, amount,
                Definitions.Accelerator.EncodeFunction("Donate"));
        }

        public AccountBlockTemplate VoteByName(Hash id, string pillarName, long vote)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts, 0,
                Definitions.Accelerator.EncodeFunction("VoteByName", id.Bytes, pillarName, vote));
        }

        public AccountBlockTemplate VoteByProdAddress(Hash id, long vote)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts, 0,
                Definitions.Accelerator.EncodeFunction("VoteByProdAddress", id.Bytes, vote));
        }
    }
}