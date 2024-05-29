# Zenon Sdk for .NET

[![nuget](https://img.shields.io/nuget/v/Zenon.Sdk)](https://nuget.org/packages/Zenon.Sdk) [![build](https://img.shields.io/github/actions/workflow/status/hypercore-one/znn_sdk_csharp/publish.yml?branch=main)](https://github.com/hypercore-one/znn_sdk_csharp/actions/workflows/publish.yml) [![codecov](https://img.shields.io/codecov/c/github/hypercore-one/znn_sdk_csharp?token=FWKGWMWO7U)](https://codecov.io/gh/hypercore-one/znn_sdk_csharp)

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
using var client = new WsClient("wss://my.hc1node.com:35998");

// Connect to node
await client.ConnectAsync();
```

### Generate wallet

```csharp
using Zenon;
using Zenon.Wallet;

var walletName = "name";
var passphrase = "secret";

// Use key store manager
var walletManager = new KeyStoreManager();

// Create wallet
var walletDefinition = walletManager
    .CreateNew(passphrase, walletName);
```

### Generate wallet from mnemonic

```csharp
using Zenon;
using Zenon.Wallet;

var walletName = "name";
var passphrase = "secret";
var mnemonic = @"route become dream access impulse price inform obtain 
    engage ski believe awful absent pig thing vibrant 
    possible exotic flee pepper marble rural fire fancy";

// Use key store manager
var walletManager = new KeyStoreManager();

// Create wallet
var walletDefinition = walletManager
    .CreateFromMnemonic(mnemonic, passphrase, walletName);
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
        await wallet.GetAccountAsync(accountIndex: 0)
};

// Send tx
await zdk.SendAsync(zdk.Embedded.Pillar.CollectReward());
```

### Receive a transaction

```csharp
using Zenon;
using Zenon.Client;
using Zenon.Wallet;
using Zenon.Model.NoM;

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
        await wallet.GetAccountAsync(accountIndex: 0)
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
        await zdk.SendAsync(AccountBlockTemplate
            .Receive(client.ProtocolVersion, client.ChainIdentifier, item.Hash));
    }
}
```

## Contributing

Please check [CONTRIBUTING](./CONTRIBUTING.md) for more details.

## License

The MIT License (MIT). Please check [LICENSE](./LICENSE) for more information.