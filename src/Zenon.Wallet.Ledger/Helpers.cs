using Zenon.Wallet.Ledger.Exceptions;
using Zenon.Wallet.Ledger.Responses;

namespace Zenon.Wallet.Ledger
{
    public static class Helpers
    {
        public static byte[] GetDerivationPathData(IAddressPath addressPath)
        {
            if (addressPath == null)
            {
                throw new ArgumentNullException(nameof(addressPath));
            }

            return GetByteData(addressPath.ToArray());
        }

        public static Exception HandleErrorResponse(ResponseBase response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            return new ResponseException(GetStatusMessage(response.ReturnCode), response.Data, response.ReturnCode);
        }

        public static string GetStatusMessage(int returnCode)
        {
            switch (returnCode)
            {
                // Incorrect length errors
                case >= 0x6801 and <= 0x6818:
                    return "Incorrect length exception occurred. The Ledger received incorrect data. This probably means that there is no app loaded.";
                // Security errors
                case >= 0x5101 and <= 0x590B:
                    return "A security exception occurred. This probably means that the user has not entered their pin, or there is no app loaded.";

                case StatusCode.Success:
                    return "Success";
                case StatusCode.Deny: // DENY
                    return "Conditions have not been satisfied for this command";
                case StatusCode.WrongP1P2: // WRONG_P1P2
                    return "Incorrect P1 or P2";
                case StatusCode.WrongDataLength: // WRONG_DATA_LENGTH
                    return "Either wrong Lc or length of APDU command less than 5";
                case StatusCode.InactiveDevice:
                    return "Device is inactive";
                case StatusCode.NotAllowed:
                    return "Not allowed";
                case StatusCode.InstructionCodeNotSupported: // INS_NOT_SUPPORTED
                    return "Instruction not supported in current app or there is no app running";
                case StatusCode.InstructionClassNotSupported: // CLA_NOT_SUPPORTED
                    return "CLA not supported in current app";
                case StatusCode.AppIsNotOpen:
                    return "App is not open";
                // App specific errors
                case StatusCode.WrongResponseLength: // WRONG_RESPONSE_LENGTH
                    return "Wrong response length (buffer too small or too big)";
                case StatusCode.DisplayBIP32PathFail: // DISPLAY_BIP32_PATH_FAIL
                    return "Failed to display BIP32 path";
                case StatusCode.DisplayAddressFail: // DISPLAY_ADDRESS_FAIL
                    return "Failed to display address";
                case StatusCode.DisplayAmountFail: // DISPLAY_AMOUNT_FAIL
                    return "Failed to display amount";
                case StatusCode.WrongTxLength: // SW_WRONG_TX_LENGTH
                    return "Wrong transaction length";
                case StatusCode.TxParsingFail: // TX_PARSING_FAIL
                    return "Failed to parse transaction";
                case StatusCode.TxHashFail: // TX_HASH_FAIL
                    return "Failed to hash transaction";
                case StatusCode.BadState: // BAD_STATE
                    return "Bad state";
                case StatusCode.SignatureFail: // SIGNATURE_FAIL
                    return "Failed to sign transaction";
                default:
                    return "Unknown error";
            }
        }

        internal static byte[] GetRequestDataPacket(Stream stream, int packetIndex)
        {
            using (var returnStream = new MemoryStream())
            {
                var position = (int)returnStream.Position;
                returnStream.WriteByte(Constants.DEFAULT_CHANNEL >> 8 & 0xff);
                returnStream.WriteByte(Constants.DEFAULT_CHANNEL & 0xff);
                returnStream.WriteByte(Constants.TAG_APDU);
                returnStream.WriteByte((byte)(packetIndex >> 8 & 0xff));
                returnStream.WriteByte((byte)(packetIndex & 0xff));

                if (packetIndex == 0)
                {
                    returnStream.WriteByte((byte)(stream.Length >> 8 & 0xff));
                    returnStream.WriteByte((byte)(stream.Length & 0xff));
                }

                var headerLength = (int)(returnStream.Position - position);
                var blockLength = Math.Min(Constants.LEDGER_HID_PACKET_SIZE - headerLength, (int)stream.Length - (int)stream.Position);

                var packetBytes = stream.ReadAllBytes(blockLength);

                returnStream.Write(packetBytes, 0, packetBytes.Length);

                while (returnStream.Length % Constants.LEDGER_HID_PACKET_SIZE != 0)
                {
                    returnStream.WriteByte(0);
                }

                return returnStream.ToArray();
            }
        }

        internal static byte[] GetResponseDataPacket(byte[] data, int packetIndex, ref int remaining)
        {
            using (var returnStream = new MemoryStream())
            {
                using (var input = new MemoryStream(data))
                {
                    var position = (int)input.Position;
                    var channel = input.ReadAllBytes(2);

                    var thirdByte = input.ReadByte();
                    if (thirdByte != Constants.TAG_APDU)
                    {
                        ThrowReadException("third", Constants.TAG_APDU, thirdByte, packetIndex);
                    }

                    var fourthByte = input.ReadByte();
                    var expectedResult = packetIndex >> 8 & 0xff;
                    if (fourthByte != expectedResult)
                    {
                        ThrowReadException("fourth", expectedResult, fourthByte, packetIndex);
                    }

                    var fifthByte = input.ReadByte();
                    expectedResult = packetIndex & 0xff;
                    if (fifthByte != expectedResult)
                    {
                        ThrowReadException("fifth", expectedResult, fifthByte, packetIndex);
                    }

                    if (packetIndex == 0)
                    {
                        remaining = input.ReadByte() << 8;
                        remaining |= input.ReadByte();
                    }

                    var headerSize = input.Position - position;
                    var blockSize = (int)Math.Min(remaining, Constants.LEDGER_HID_PACKET_SIZE - headerSize);

                    var commandPart = new byte[blockSize];
                    if (input.Read(commandPart, 0, commandPart.Length) != commandPart.Length)
                    {
                        throw new ManagerException("Reading from the Ledger failed. The data read was not of the correct size. It is possible that the incorrect Hid device has been used. Please check that the Hid device with the correct UsagePage was selected");
                    }

                    returnStream.Write(commandPart, 0, commandPart.Length);

                    remaining -= blockSize;

                    return returnStream.ToArray();
                }
            }
        }

        private static void ThrowReadException(string bytePosition, int expected, int actual, int packetIndex)
        {
            throw new ManagerException($"Reading from the Ledger failed. The {bytePosition} byte was incorrect. Expected: {expected} Actual: {actual} Packet Index: {packetIndex}. It is possible that the incorrect Hid device has been used. Please check that the Hid device with the correct UsagePage was selected");
        }

        private static byte[] GetByteData(uint[] indices)
        {
            byte[] addressIndicesData;
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.WriteByte((byte)indices.Length);
                for (var i = 0; i < indices.Length; i++)
                {
                    var data = indices[i].ToBytes();
                    memoryStream.Write(data, 0, data.Length);
                }
                addressIndicesData = memoryStream.ToArray();
            }

            return addressIndicesData;
        }
    }
}
