using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Zenon
{
    public class ZnnPaths
    {
        public static ZnnPaths Default;

        static ZnnPaths()
        {
            string main;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                main = Path.Join(Environment.GetEnvironmentVariable("HOME"),
                    $".{Constants.ZnnRootDirectory}");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                main = Path.Join(Environment.GetEnvironmentVariable("HOME"),
                    "Library", Constants.ZnnRootDirectory);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                main = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    Constants.ZnnRootDirectory);
            }
            else
            {
                main = Path.Join(Environment.GetEnvironmentVariable("HOME"),
                    Constants.ZnnRootDirectory);
            }

            Default = new ZnnPaths(main,
                Path.Join(main, "wallet"),
                Path.Join(main, "syrius"));
        }

        public ZnnPaths(string main, string wallet, string cache)
        {
            Main = main;
            Wallet = wallet;
            Cache = cache;
        }

        public string Main { get; }
        public string Wallet { get; }
        public string Cache { get; }
    }
}