using System;

namespace Zenon.Client
{
    public static class Utils
    {
        public static bool ValidateWsConnectionrUrl(string urlString)
        {
            Uri url;
            return Uri.TryCreate(urlString, UriKind.Absolute, out url) &&
                url.IsAbsoluteUri &&
                url.Scheme == "ws" || url.Scheme == "wss";
        }
    }
}
