using System.Diagnostics;

namespace Zenon.Client
{
    public class WsClientOptions
    {
        public int ProtocolVersion { get; set; }
        public int ChainIdentifier { get; set; }
        public SourceLevels TraceSourceLevels { get; set; }
    }
}
