using Zenon.Api;
using Zenon.Api.Embedded;
using Zenon.Client;
using Zenon.Model.Primitives;

var client = new WsClient();
{
    await client.StartAsync(new Uri("ws://nodes.zenon.place:35998"));

    var pillarList = await new PillarApi(client)
        .GetAll();

    Console.WriteLine($"Number of pillars: {pillarList.Count}");

    var accountInfo = await new LedgerApi(client)
        .GetAccountInfoByAddress(Address.Parse("z1qq0hffeyj0htmnr4gc6grd8zmqfvwzgrydt402"));

    Console.WriteLine($"Account info: {accountInfo}");

    await client.StopAsync();
}