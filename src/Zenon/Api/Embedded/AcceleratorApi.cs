using Newtonsoft.Json.Linq;
using System.Linq;
using System.Numerics;
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
            var response = await Client.SendRequestAsync<JProjectList>("embedded.accelerator.getAll", pageIndex, pageSize);
            return new ProjectList(response);
        }

        public async Task<Project> GetProjectById(Hash id)
        {
            var response = await Client.SendRequestAsync<JProject>("embedded.accelerator.getProjectById", id.ToString());
            return new Project(response);
        }

        public async Task<Phase> GetPhaseById(Hash id)
        {
            var response = await Client.SendRequestAsync<JObject>("embedded.accelerator.getPhaseById", id.ToString());
            return new Phase(JPhase.FromJObject(response));
        }

        public async Task<PillarVote[]> GetPillarVotes(string name, string[] hashes)
        {
            var response = await Client.SendRequestAsync<JPillarVote[]>("embedded.accelerator.getPillarVotes", name, hashes);
            return response.Select(x => new PillarVote(x)).ToArray();
        }

        public async Task<VoteBreakdown> GetVoteBreakdown(Hash id)
        {
            var response = await Client.SendRequestAsync<JVoteBreakdown>("embedded.accelerator.getVoteBreakdown", id.ToString());
            return new VoteBreakdown(response);
        }

        // Contract methods
        public AccountBlockTemplate CreateProject(string name, string description,
            string url, BigInteger znnFundsNeeded, BigInteger qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.AcceleratorAddress, TokenStandard.ZnnZts,
                Constants.ProjectCreationFeeInZnn,
                Definitions.Accelerator.EncodeFunction("CreateProject", name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate AddPhase(Hash id, string name, string description,
            string url, BigInteger znnFundsNeeded, BigInteger qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.AcceleratorAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Accelerator.EncodeFunction("AddPhase", id.Bytes, name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate UpdatePhase(Hash id, string name, string description,
            string url, BigInteger znnFundsNeeded, BigInteger qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.AcceleratorAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Accelerator.EncodeFunction("UpdatePhase", id.Bytes, name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate Donate(BigInteger amount, TokenStandard zts)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.AcceleratorAddress, zts, amount,
                Definitions.Accelerator.EncodeFunction("Donate"));
        }

        public AccountBlockTemplate VoteByName(Hash id, string pillarName, int vote)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.AcceleratorAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Accelerator.EncodeFunction("VoteByName", id.Bytes, pillarName, vote));
        }

        public AccountBlockTemplate VoteByProdAddress(Hash id, int vote)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier,
                Address.AcceleratorAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Accelerator.EncodeFunction("VoteByProdAddress", id.Bytes, vote));
        }
    }
}