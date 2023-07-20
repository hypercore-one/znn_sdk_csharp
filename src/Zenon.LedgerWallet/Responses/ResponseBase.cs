namespace Zenon.LedgerWallet.Responses
{
    public abstract class ResponseBase
    {
        private const int HardeningConstant = 0xff;

        public byte[] Data { get; }
        public bool IsSuccess => ReturnCode == Constants.SuccessStatusCode;
        public int ReturnCode { get; }

        public string StatusMessage
        {
            get
            {
                switch (ReturnCode)
                {
                    case Constants.SuccessStatusCode:
                        return "Success";
                    case Constants.DenyStatusCode: // DENY
                        return "Conditions have not been satisfied for this command";
                    case Constants.WrongP1P2StatusCode: // WRONG_P1P2
                        return "Incorrect P1 or P2";
                    case Constants.WrongDataLengthStatusCode: // WRONG_DATA_LENGTH
                        return "Either wrong Lc or length of APDU command less than 5";
                    case Constants.InstructionCodeNotSupportedStatusCode: // INS_NOT_SUPPORTED
                        return "Instruction not supported in current app or there is no app running";
                    case Constants.InstructionClassNotSupportedStatusCode: // CLA_NOT_SUPPORTED
                        return "CLA not supported in current app";
                    // App specific errors
                    case Constants.WrongResponseLengthStatusCode: // WRONG_RESPONSE_LENGTH
                        return "Wrong response length (buffer too small or too big)";
                    case Constants.DisplayBIP32PathFailStatusCode: // DISPLAY_BIP32_PATH_FAIL
                        return "Failed to display BIP32 path";
                    case Constants.DisplayAddressFailStatusCode: // DISPLAY_ADDRESS_FAIL
                        return "Failed to display address";
                    case Constants.DisplayAmountFailStatusCode: // DISPLAY_AMOUNT_FAIL
                        return "Failed to display amount";
                    case Constants.WrongTxLengthStatusCode: // SW_WRONG_TX_LENGTH
                        return "Wrong transaction length";
                    case Constants.TxParsingFailStatusCode: // TX_PARSING_FAIL
                        return "Failed to parse transaction";
                    case Constants.TxHashFailStatusCode: // TX_HASH_FAIL
                        return "Failed to hash transaction";
                    case Constants.BadStateStatusCode: // BAD_STATE
                        return "Bad state";
                    case Constants.SignatureFailStatusCode: // SIGNATURE_FAIL
                        return "Failed to sign transaction";
                    default:
                        return "Unknown error";
                }
            }
        }

        protected ResponseBase(byte[] data)
        {
            Data = data;
            var returnCode = GetReturnCode(data);
            ReturnCode = returnCode;
        }

        public static int GetReturnCode(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return ((data[data.Length - 2] & HardeningConstant) << 8) | (data[data.Length - 1] & HardeningConstant);
        }
    }
}
