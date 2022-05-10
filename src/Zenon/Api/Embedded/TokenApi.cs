using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Model.NoM;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class TokenApi
    {
        public TokenApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<TokenList> GetAll(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JTokenList>("embedded.token.getAll", pageIndex, pageSize);
            return new TokenList(response);
        }

        public async Task<TokenList> GetByOwner(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequest<JTokenList>("embedded.token.getByOwner", address.ToString(), pageIndex, pageSize);
            return new TokenList(response);
        }

        public async Task<TokenList> GetByZts(TokenStandard tokenStandard)
        {
            var response = await Client.SendRequest<JTokenList>("embedded.token.getByZts", tokenStandard.ToString());
            return response != null ? new TokenList(response) : null;
        }
    }
}
