using Newtonsoft.Json.Linq;
using System;
using System.Linq;
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
    public class AcceleratorApi
    {
        public AcceleratorApi(Lazy<IClient> client)
        {
            Client = client;
        }

        public Lazy<IClient> Client { get; }

        public async Task<ProjectList> GetAll(uint pageIndex = 0, uint pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.Value.SendRequest<JProjectList>("embedded.accelerator.getAll", pageIndex, pageSize);
            return new ProjectList(response);
        }

        public async Task<Project> GetProjectById(Hash id)
        {
            var response = await Client.Value.SendRequest<JProject>("embedded.accelerator.getProjectById", id.ToString());
            return new Project(response);
        }

        public async Task<Phase> GetPhaseById(Hash id)
        {
            var response = await Client.Value.SendRequest<JObject>("embedded.accelerator.getPhaseById", id.ToString());
            return new Phase(JPhase.FromJObject(response));
        }

        public async Task<PillarVote[]> GetPillarVotes(string name, string[] hashes)
        {
            var response = await Client.Value.SendRequest<JPillarVote[]>("embedded.accelerator.getPillarVotes", name, hashes);
            return response.Select(x => new PillarVote(x)).ToArray();
        }

        public async Task<VoteBreakdown> GetVoteBreakdown(Hash id)
        {
            var response = await Client.Value.SendRequest<JVoteBreakdown>("embedded.accelerator.getVoteBreakdown", id.ToString());
            return new VoteBreakdown(response);
        }

        // Contract methods
        public AccountBlockTemplate CreateProject(string name, string description,
            string url, BigInteger znnFundsNeeded, BigInteger qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts,
                Constants.ProjectCreationFeeInZnn,
                Definitions.Accelerator.EncodeFunction("CreateProject", name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate AddPhase(Hash id, string name, string description,
            string url, BigInteger znnFundsNeeded, BigInteger qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Accelerator.EncodeFunction("AddPhase", id.Bytes, name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate UpdatePhase(Hash id, string name, string description,
            string url, BigInteger znnFundsNeeded, BigInteger qsrFundsNeeded)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Accelerator.EncodeFunction("UpdatePhase", id.Bytes, name, description, url, znnFundsNeeded, qsrFundsNeeded));
        }

        public AccountBlockTemplate Donate(BigInteger amount, TokenStandard zts)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, zts, amount,
                Definitions.Accelerator.EncodeFunction("Donate"));
        }

        public AccountBlockTemplate VoteByName(Hash id, string pillarName, int vote)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Accelerator.EncodeFunction("VoteByName", id.Bytes, pillarName, vote));
        }

        public AccountBlockTemplate VoteByProdAddress(Hash id, int vote)
        {
            return AccountBlockTemplate.CallContract(Address.AcceleratorAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Accelerator.EncodeFunction("VoteByProdAddress", id.Bytes, vote));
        }
    }
}