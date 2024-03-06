using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Zenon.Wallet.Json
{
    public class JEncryptedFile
    {
        public static JEncryptedFile FromJObject(JObject json)
        {
            var data = json.DeepClone() as JObject;
            var file = new JEncryptedFile();
            file.crypto = data["crypto"] != null ? data["crypto"].ToObject<JCryptoData>() : null;
            data.Remove("crypto");
            file.timestamp = data.Value<int>("timestamp");
            data.Remove("timestamp");
            file.version = data.Value<int>("version");
            data.Remove("version");
            file.metadata = data.Count != 0 ? data.ToObject<Dictionary<string, dynamic>>() : null;
            return file;
        }

        public static JObject ToJObject(JEncryptedFile file)
        {
            var json = file.metadata != null ? JObject.FromObject(file.metadata) : new JObject();
            if (file.crypto != null)
            {
                json["crypto"] = JObject.FromObject(file.crypto);
            }
            json["timestamp"] = file.timestamp;
            json["version"] = file.version;
            return json;
        }

        public Dictionary<string, dynamic> metadata { get; set; }
        public JCryptoData crypto { get; set; }
        public int timestamp { get; set; }
        public int version { get; set; }
    }
}