# Zenon Sdk for .NET

[![nuget](https://img.shields.io/nuget/v/Zenon.Sdk)](https://nuget.org/packages/Zenon.Sdk) [![build](https://img.shields.io/github/actions/workflow/status/kingGorrin/znn_sdk_csharp/publish.yml?branch=main)](https://github.com/KingGorrin/znn_sdk_csharp/actions/workflows/publish.yml) [![codecov](https://img.shields.io/codecov/c/github/KingGorrin/znn_sdk_csharp?token=FWKGWMWO7U)](https://codecov.io/gh/KingGorrin/znn_sdk_csharp)

Reference implementation for the Zenon SDK for .NET. Compatible with the Zenon Alphanet - Network of Momentum Phase 1. 
It provides a simple integration with any .NET based projects.

## Installation

Install the Zenon.Sdk package from [NuGet](https://www.nuget.org/packages/Zenon.Sdk)

```
dotnet add package Zenon.Sdk
```

## Usage

### Connect node

```csharp
using Zenon;

var nodeUrl = new Uri("ws://my.hc1node.com:35998");

var znnClient = Znn.Instance;

await znnClient.Client.Value.StartAsync(nodeUrl);
...
await znnClient.Client.Value.StopAsync();
```

### Generate wallet

```csharp
using Zenon;

var wallet = "name";
var passphrase = "secret";

var znnClient = Znn.Instance;

znnClient.DefaultKeyStorePath = 
    znnClient.KeyStoreManager.CreateNew(passphrase, wallet);
znnClient.DefaultKeyStore = 
    znnClient.KeyStoreManager.ReadKeyStore(passphrase, znnClient.DefaultKeyStorePath);
```

### Generate wallet from mnemonic

```csharp
using Zenon;

var wallet = "name";
var passphrase = "secret";
var mnemonic =
      "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";

var znnClient = Znn.Instance;

znnClient.DefaultKeyStorePath = 
    znnClient.KeyStoreManager.CreateFromMnemonic(mnemonic, passphrase, wallet);
znnClient.DefaultKeyStore = 
    znnClient.KeyStoreManager.ReadKeyStore(passphrase, znnClient.DefaultKeyStorePath);
```

### Sending a transaction

```csharp
using Zenon;

var nodeUrl = new Uri("ws://my.hc1node.com:35998");
var wallet = "name";
var passphrase = "secret";
var mnemonic =
      "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";

var znnClient = Znn.Instance;

znnClient.DefaultKeyStorePath = 
    znnClient.KeyStoreManager.CreateFromMnemonic(mnemonic, passphrase, wallet);
znnClient.DefaultKeyStore = 
    znnClient.KeyStoreManager.ReadKeyStore(passphrase, znnClient.DefaultKeyStorePath);
znnClient.DefaultKeyPair = 
    await znnClient.DefaultKeyStore.GetSignerAsync(0); // Use primary address

await znnClient.Client.Value.StartAsync(nodeUrl);

var tx = znnClient.Embedded.Pillar.CollectReward();

await znnClient.Send(tx);

await znnClient.Client.Value.StopAsync();
```

### Receive a transaction

```csharp
using Zenon;
using Zenon.Model.NoM;

var nodeUrl = new Uri("ws://my.hc1node.place:35998");
var wallet = "name";
var passphrase = "secret";
var mnemonic =
      "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";

var znnClient = Znn.Instance;

znnClient.DefaultKeyStorePath = 
    znnClient.KeyStoreManager.CreateFromMnemonic(mnemonic, passphrase, wallet);
znnClient.DefaultKeyStore = 
    znnClient.KeyStoreManager.ReadKeyStore(passphrase, znnClient.DefaultKeyStorePath);
znnClient.DefaultKeyPair = 
    await znnClient.DefaultKeyStore.GetSignerAsync(0); // Use primary address

await znnClient.Client.Value.StartAsync(nodeUrl);

var result = await znnClient.Ledger
    .GetUnreceivedBlocksByAddress(await znnClient.DefaultKeyPair.GetAddressAsync());

if (result.Count != 0)
{
    foreach (var item in result.List)
    {
        var tx = AccountBlockTemplate.Receive(item.Hash);

        await znnClient.Send(tx);
    }
}

await znnClient.Client.Value.StopAsync();
```

## Contributing

Please check [CONTRIBUTING](./CONTRIBUTING.md) for more details.

## License

The MIT License (MIT). Please check [LICENSE](./LICENSE) for more information.