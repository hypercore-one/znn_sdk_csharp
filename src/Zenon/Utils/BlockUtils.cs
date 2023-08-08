using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Zenon.Model.Embedded;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;
using Zenon.Pow;
using Zenon.Wallet;

namespace Zenon.Utils
{
    public static class BlockUtils
    {
        public static bool IsSendBlock(BlockTypeEnum blockType)
        {
            return blockType == BlockTypeEnum.UserSend ||
                blockType == BlockTypeEnum.ContractSend;
        }

        public static bool IsReceiveBlock(BlockTypeEnum blockType)
        {
            return blockType == BlockTypeEnum.UserReceive ||
                blockType == BlockTypeEnum.GenesisReceive ||
                blockType == BlockTypeEnum.ContractReceive;
        }

        public static Hash GetTransactionHash(AccountBlockTemplate transaction)
        {
            return Hash.Digest(GetTransactionBytes(transaction));
        }

        public static byte[] GetTransactionBytes(AccountBlockTemplate transaction)
        {
            var versionBytes = BytesUtils.GetBytes((long)transaction.Version);
            var chainIdentifierBytes = BytesUtils.GetBytes((long)transaction.ChainIdentifier);
            var blockTypeBytes = BytesUtils.GetBytes((long)transaction.BlockType);
            var previousHashBytes = transaction.PreviousHash.Bytes;
            var heightBytes = BytesUtils.GetBytes(transaction.Height);
            var momentumAcknowledgedBytes = transaction.MomentumAcknowledged.GetBytes();
            var addressBytes = transaction.Address.Bytes;
            var toAddressBytes = transaction.ToAddress.Bytes;
            var amountBytes = BytesUtils.BigIntToBytes(transaction.Amount, 32);
            var tokenStandardBytes = transaction.TokenStandard.Bytes;
            var fromBlockHashBytes = transaction.FromBlockHash.Bytes;
            var descendentBlocksBytes = Hash.Digest(new byte[0]).Bytes;
            var dataBytes = Hash.Digest(transaction.Data).Bytes;
            var fusedPlasmaBytes = BytesUtils.GetBytes(transaction.FusedPlasma);
            var difficultyBytes = BytesUtils.GetBytes(transaction.Difficulty);
            var nonceBytes = BytesUtils.LeftPadBytes(BytesUtils.FromHexString(transaction.Nonce), 8);

            var source = ArrayUtils.Concat(
                versionBytes,
                chainIdentifierBytes,
                blockTypeBytes,
                previousHashBytes,
                heightBytes,
                momentumAcknowledgedBytes,
                addressBytes,
                toAddressBytes,
                amountBytes,
                tokenStandardBytes,
                fromBlockHashBytes,
                descendentBlocksBytes,
                dataBytes,
                fusedPlasmaBytes,
                difficultyBytes,
                nonceBytes
            );

            return source;
        }

        private static Hash GetPoWData(AccountBlockTemplate transaction)
        {
            return Hash.Digest(ArrayUtils.Concat(transaction.Address.Bytes, transaction.PreviousHash.Bytes));
        }

        private static async Task AutofillTransactionParametersAsync(Zdk zdk, AccountBlockTemplate accountBlockTemplate)
        {
            var frontierAccountBlock =
                await zdk.Ledger.GetFrontierAccountBlock(accountBlockTemplate.Address);

            long height = 1;
            Hash previousHash = Hash.Empty;

            if (frontierAccountBlock != null)
            {
                height = frontierAccountBlock.Height + 1;
                previousHash = frontierAccountBlock.Hash;
            }

            accountBlockTemplate.Height = height;
            accountBlockTemplate.PreviousHash = previousHash;

            var frontierMomentum = await zdk.Ledger.GetFrontierMomentum();

            accountBlockTemplate.MomentumAcknowledged =
                new HashHeight(frontierMomentum.Hash, frontierMomentum.Height);
        }

        private static async Task<bool> CheckAndSetFieldsAsync(Zdk zdk, AccountBlockTemplate transaction, IWalletAccount currentAccount)
        {
            transaction.Address = await currentAccount.GetAddressAsync();
            transaction.PublicKey = await currentAccount.GetPublicKeyAsync();

            await AutofillTransactionParametersAsync(zdk, transaction);

            if (BlockUtils.IsSendBlock(transaction.BlockType))
            {

            }
            else
            {
                if (transaction.FromBlockHash == Hash.Empty)
                {
                    throw new Exception();
                }

                var sendBlock = await zdk.Ledger.GetAccountBlockByHash(transaction.FromBlockHash);

                if (sendBlock == null)
                {
                    throw new Exception();
                }
                if (sendBlock.ToAddress.ToString() != transaction.Address.ToString())
                {
                    throw new Exception();
                }

                if (transaction.Data == null || transaction.Data.Length != 0)
                {
                    throw new Exception();
                }
            }

            if (transaction.Difficulty > 0 && transaction.Nonce == "")
            {
                throw new Exception();
            }

            return true;
        }

        private static async Task<bool> SetDifficultyAsync(Zdk zdk, AccountBlockTemplate transaction,
            Action<PowStatus> generatingPowCallback, bool waitForRequiredPlasma = false)
        {
            var powParam = new GetRequiredParam(
                transaction.Address,
                transaction.BlockType,
                transaction.ToAddress,
                transaction.Data);

            var response =
                await zdk.Embedded.Plasma.GetRequiredPoWForAccountBlock(powParam);

            if (response.RequiredDifficulty != 0)
            {
                transaction.FusedPlasma = response.AvailablePlasma;
                transaction.Difficulty = response.RequiredDifficulty;

                Debug.WriteLine($"Generating Plasma for block: hash={GetPoWData(transaction)}");

                generatingPowCallback(PowStatus.Generating);

                transaction.Nonce = await PoW.Generate(
                    GetPoWData(transaction), transaction.Difficulty);

                generatingPowCallback(PowStatus.Done);
            }
            else
            {
                transaction.FusedPlasma = response.BasePlasma;
                transaction.Difficulty = 0;
                transaction.Nonce = "0000000000000000";
            }
            return true;
        }

        private static async Task<bool> SetHashAndSignatureAsync(AccountBlockTemplate transaction, IWalletAccount currentAccount)
        {
            transaction.Hash = GetTransactionHash(transaction);
            transaction.Signature = await currentAccount.SignTxAsync(transaction); ;

            return true;
        }

        public static async Task<AccountBlockTemplate> SendAsync(Zdk zdk, AccountBlockTemplate transaction,
            IWalletAccount currentAccount, Action<PowStatus> generatingPowCallback, bool waitForRequiredPlasma = false)
        {
            await CheckAndSetFieldsAsync(zdk, transaction, currentAccount);
            await SetDifficultyAsync(zdk, transaction, generatingPowCallback, waitForRequiredPlasma);

            await SetHashAndSignatureAsync(transaction, currentAccount);

            await zdk.Ledger.PublishRawTransaction(transaction);

            Debug.WriteLine("Published account-block");

            return transaction;
        }

        public static async Task<bool> RequiresPoWAsync(Zdk zdk, AccountBlockTemplate transaction, IWalletAccount account)
        {
            transaction.Address = await account.GetAddressAsync();

            var powParam = new GetRequiredParam(
                transaction.Address,
                transaction.BlockType,
                transaction.ToAddress,
                transaction.Data);

            var response =
                await zdk.Embedded.Plasma.GetRequiredPoWForAccountBlock(powParam);

            if (response.RequiredDifficulty == 0)
            {
                return false;
            }
            return true;
        }
    }
}
