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

            switch (response.ReturnCode)
            {
                case >= 0x6E00 and <= 0x6E02:
                case >= 0x6D00 and <= 0x6D06:
                    return new InstructionNotSupportedException(response.Data, response.ReturnCode);
                case >= 0x6801 and <= 0x6818:
                    return new IncorrectLengthException(response.Data, response.ReturnCode);
                case >= 0x5101 and <= 0x590B:
                    return new SecurityException(response.Data, response.ReturnCode);
                default:
                    return new InvalidAPDUResponseException(response.StatusMessage, response.Data, response.ReturnCode);
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
