# Zenon Sdk for .NET

[![nuget](https://img.shields.io/nuget/vpre/Zenon.Sdk)](https://nuget.org/packages/Zenon.Sdk) [![build](https://img.shields.io/github/workflow/status/kingGorrin/znn_sdk_csharp/Zenon.Sdk.NET)](https://github.com/KingGorrin/znn_sdk_csharp/actions/workflows/publish.yml) [![codecov](https://img.shields.io/codecov/c/github/KingGorrin/znn_sdk_csharp?token=FWKGWMWO7U)](https://codecov.io/gh/KingGorrin/znn_sdk_csharp)

Reference implementation for the Zenon SDK for .NET. Compatible with the Zenon Alphanet - Network of Momentum Phase 0. 
It provides a simple integration with any .NET based projects

## Installation

Install the Zenon.Sdk package from [NuGet](https://www.nuget.org/packages/Zenon.Sdk)

```
dotnet add package Zenon.Sdk --prerelease
```

## Usage

## Examples

### Example 1 - APIs

```csharp
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
```

## Contributing

Please check [CONTRIBUTING](./CONTRIBUTING.md) for more details.

## License

The MIT License (MIT). Please check [LICENSE](./LICENSE) for more information.