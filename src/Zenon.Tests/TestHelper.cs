using System.IO;
using System.Reflection;
using Zenon.Model.NoM;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives.Json;

namespace Zenon
{
    internal static class TestHelper
    {
        internal static string GetManifestResourceText(string resourceName)
        {
            return GetManifestResourceText(Assembly.GetCallingAssembly(), resourceName);
        }

        internal static string GetManifestResourceText(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream!))
                return reader.ReadToEnd();
        }

        internal static AccountBlockTemplate CreateAccountBlockTemplate(long amount, string data)
        {
            return CreateAccountBlockTemplate("z1qxemdeddedxaccelerat0rxxxxxxxxxxp4tk22", "zts1znnxxxxxxxxxxxxx9z4ulx", amount, data);
        }

        internal static AccountBlockTemplate CreateAccountBlockTemplate(string toAddres, string tokenStandard, long amount, string data)
        {
            return new AccountBlockTemplate(
                new JAccountBlockTemplate()
                {
                    version = 1,
                    chainIdentifier = 1,
                    blockType = 2,
                    hash = "0000000000000000000000000000000000000000000000000000000000000000",
                    previousHash = "0000000000000000000000000000000000000000000000000000000000000000",
                    height = 0,
                    momentumAcknowledged = new JHashHeight()
                    {
                        hash = "0000000000000000000000000000000000000000000000000000000000000000",
                        height = 0
                    },
                    address = "z1qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqsggv2f",
                    toAddress = toAddres,
                    amount = amount,
                    tokenStandard = tokenStandard,
                    fromBlockHash = "0000000000000000000000000000000000000000000000000000000000000000",
                    data = data,
                    fusedPlasma = 0,
                    difficulty = 0,
                    nonce = "",
                    publicKey = "",
                    signature = ""
                });
        }
    }
}
