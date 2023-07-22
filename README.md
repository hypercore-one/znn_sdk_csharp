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
using Zenon.Client;

// Create client instance with default options (mainnet)
using var mainnet = new WsClient("wss://my.hc1node.com:35998");

// Connect to node
await mainnet.ConnectAsync();
...
// Disconnect from node
await mainnet.CloseAsync();
```

### Generate wallet

```csharp
using Zenon;
using Zenon.Wallet;

var wallet = "name";
var passphrase = "secret";

// Use the key store manager
var walletManager = new KeyStoreManager();

// Create wallet
var walletDefinition = walletManager
    .CreateNew(passphrase, wallet);
```

### Generate wallet from mnemonic

```csharp
using Zenon;
using Zenon.Wallet;

var wallet = "name";
var passphrase = "secret";
var mnemonic =
        "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";

// Use the key store manager
var walletManager = new KeyStoreManager();

// Create wallet
var walletDefinition = walletManager
    .CreateFromMnemonic(mnemonic, passphrase, wallet);
```

### Sending a transaction

```csharp
using Zenon;
using Zenon.Client;
using Zenon.Wallet;

// Use key store manager
var walletManager = new KeyStoreManager();

// Use first wallet available
var walletDefinition =
    (await walletManager.GetWalletDefinitionsAsync()).First();

// Options for retrieving the wallet
var options = new KeyStoreOptions()
{
    DecryptionPassword = "secret"
};

// Retrieve the wallet
var wallet =
    await walletManager.GetWalletAsync(walletDefinition, options);

// Create client instance with default options (mainnet)
using var client = new WsClient("wss://my.hc1node.com:35998");

// Connect to node
await client.ConnectAsync();

// Create zdk instance and attach primary wallet account
var zdk = new Zdk(client)
{
    DefaultWalletAccount =
        await wallet.GetAccountAsync()
};

// Send tx
await zdk.SendAsync(zdk.Embedded.Pillar.CollectReward());
...
// Disconnect from node
await client.CloseAsync();
```

### Receive a transaction

```csharp
using Zenon;
using Zenon.Client;
using Zenon.Wallet;
using Zenon.Model.NoM;

// Use the key store manager
var walletManager = new KeyStoreManager();

// Use the first wallet available
var walletDefinition =
    (await walletManager.GetWalletDefinitionsAsync()).First();

// Options for retrieving the wallet
var options = new KeyStoreOptions()
{
    DecryptionPassword = "secret"
};

// Retrieve the wallet
var wallet =
    await walletManager.GetWalletAsync(walletDefinition, options);

// Create client instance with default options (mainnet)
using var client = new WsClient("wss://my.hc1node.com:35998");

// Connect to node
await client.ConnectAsync();

// Create zdk instance and attach primary wallet account
var zdk = new Zdk(client)
{
    DefaultWalletAccount =
        await wallet.GetAccountAsync()
};

// Get account address
var address = await zdk.DefaultWalletAccount.GetAddressAsync();

// Get all unreceived tx's
var result = await zdk.Ledger
    .GetUnreceivedBlocksByAddress(address);

if (result.Count != 0)
{
    foreach (var item in result.List)
    {
        // Send tx
        await zdk.SendAsync(AccountBlockTemplate.Receive(client.ProtocolVersion, client.ChainIdentifier, item.Hash));
    }
}
...
// Disconnect from node
await client.CloseAsync();
```

## Contributing

Please check [CONTRIBUTING](./CONTRIBUTING.md) for more details.

## License

The MIT License (MIT). Please check [LICENSE](./LICENSE) for more information.