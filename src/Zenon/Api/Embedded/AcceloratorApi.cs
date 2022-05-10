using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.Embedded;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

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
    }
}
