﻿namespace Zenon.Model.NoM.Json
{
    public class JAccountBlock : JAccountBlockTemplate
    {
        public JAccountBlock[] descendantBlocks { get; set; }
        public ulong basePlasma { get; set; }
        public ulong usedPlasma { get; set; }
        public string changesHash { get; set; }
        public JToken token { get; set; }
        public JAccountBlockConfirmationDetail confirmationDetail { get; set; }
        public JAccountBlock pairedAccountBlock { get; set; }
    }
}
