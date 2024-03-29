﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Zenon
{
    public class ZdkPaths
    {
        public static readonly ZdkPaths Default =
            new ZdkPaths(new DotNetRuntimeInformation(), new DotNetEnvironment());

        public ZdkPaths(IRuntimeInformation runtimeInfo, IEnvironment environment)
        {
            string main;

            if (runtimeInfo.IsOSPlatform(OSPlatform.Linux))
            {
                main = Path.Join(environment.GetEnvironmentVariable("HOME"),
                    $".{Constants.ZnnRootDirectory}");
            }
            else if (runtimeInfo.IsOSPlatform(OSPlatform.OSX))
            {
                main = Path.Join(environment.GetEnvironmentVariable("HOME"),
                    "Library", Constants.ZnnRootDirectory);
            }
            else if (runtimeInfo.IsOSPlatform(OSPlatform.Windows))
            {
                main = Path.Join(environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    Constants.ZnnRootDirectory);
            }
            else
            {
                main = Path.Join(environment.GetEnvironmentVariable("HOME"),
                    Constants.ZnnRootDirectory);
            }

            Main = main;
            Wallet = Path.Join(main, "wallet");
            Cache = Path.Join(main, "syrius");
        }

        public ZdkPaths(string main, string wallet, string cache)
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