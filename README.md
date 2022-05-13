# Zenon Sdk for .NET

[![NuGet package](https://img.shields.io/nuget/v/Zenon.Sdk.svg)](https://nuget.org/packages/Zenon.Sdk) [![Zenon.Sdk.NET](https://github.com/KingGorrin/znn_sdk_csharp/actions/workflows/publish.yml/badge.svg)](https://github.com/KingGorrin/znn_sdk_csharp/actions/workflows/publish.yml) [![codecov](https://codecov.io/gh/KingGorrin/znn_sdk_csharp/branch/main/graph/badge.svg?token=FWKGWMWO7U)](https://codecov.io/gh/KingGorrin/znn_sdk_csharp)

Reference implementation for the Zenon SDK for .NET. Compatible with the Zenon Alphanet - Network of Momentum Phase 0. 
It provides a simple integration with any .NET based projects

## Installation

Install the Zenon.Sdk package from [NuGet](https://www.nuget.org/packages/Zenon.Sdk)

```
dotnet add package Zenon.Sdk
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