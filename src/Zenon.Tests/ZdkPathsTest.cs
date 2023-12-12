using FluentAssertions;
using Moq;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace Zenon
{
    public class ZdkPathsTest
    {
        public class Windows
        {
            [Fact]
            public void AssertDefaults()
            {
                // Setup
                var root = Path.Join("c:", "users", "professorz", "appdata", "roaming");
                var main = Path.Join(root, "znn");
                var wallet = Path.Join(main, "wallet");
                var cache = Path.Join(main, "syrius");

                var runtime = new Mock<IRuntimeInformation>();
                var environment = new Mock<IEnvironment>(MockBehavior.Strict);

                runtime.Setup(x => x.IsOSPlatform(OSPlatform.Windows))
                    .Returns(true);

                environment
                    .Setup(x => x.GetFolderPath(Environment.SpecialFolder.ApplicationData))
                    .Returns(root);

                // Execute
                var paths = new ZdkPaths(runtime.Object, environment.Object);

                // Validate
                main.Should().BeEquivalentTo(paths.Main);
                wallet.Should().BeEquivalentTo(paths.Wallet);
                cache.Should().BeEquivalentTo(paths.Cache);
            }
        }

        public class Unix
        {
            [Fact]
            public void AssertDefaults()
            {
                // Setup
                var root = Path.Join("home", "professorz");
                var main = Path.Join(root, ".znn");
                var wallet = Path.Join(main, "wallet");
                var cache = Path.Join(main, "syrius");

                var runtime = new Mock<IRuntimeInformation>();
                var environment = new Mock<IEnvironment>(MockBehavior.Strict);

                runtime.Setup(x => x.IsOSPlatform(OSPlatform.Linux))
                    .Returns(true);

                environment
                    .Setup(x => x.GetEnvironmentVariable("HOME"))
                    .Returns(root);

                // Execute
                var paths = new ZdkPaths(runtime.Object, environment.Object);

                // Validate
                main.Should().BeEquivalentTo(paths.Main);
                wallet.Should().BeEquivalentTo(paths.Wallet);
                cache.Should().BeEquivalentTo(paths.Cache);
            }
        }

        public class Mac
        {
            [Fact]
            public void AssertDefaults()
            {
                // Setup
                var root = Path.Join("users", "professorz");
                var main = Path.Join(root, "library", "znn");
                var wallet = Path.Join(main, "wallet");
                var cache = Path.Join(main, "syrius");

                var runtime = new Mock<IRuntimeInformation>();
                var environment = new Mock<IEnvironment>(MockBehavior.Strict);

                runtime.Setup(x => x.IsOSPlatform(OSPlatform.OSX))
                    .Returns(true);

                environment
                    .Setup(x => x.GetEnvironmentVariable("HOME"))
                    .Returns(root);

                // Execute
                var paths = new ZdkPaths(runtime.Object, environment.Object);

                // Validate
                main.Should().BeEquivalentTo(paths.Main);
                wallet.Should().BeEquivalentTo(paths.Wallet);
                cache.Should().BeEquivalentTo(paths.Cache);
            }
        }

        public class Unknown
        {
            [Fact]
            public void AssertDefaults()
            {
                // Setup
                var root = "";
                var main = Path.Join(root, "znn");
                var wallet = Path.Join(main, "wallet");
                var cache = Path.Join(main, "syrius");

                var runtime = new Mock<IRuntimeInformation>();
                var environment = new Mock<IEnvironment>(MockBehavior.Strict);

                runtime.Setup(x => x.IsOSPlatform(It.IsAny<OSPlatform>()))
                    .Returns(false);

                environment
                    .Setup(x => x.GetEnvironmentVariable("HOME"))
                    .Returns(root);

                // Execute
                var paths = new ZdkPaths(runtime.Object, environment.Object);

                // Validate
                main.Should().BeEquivalentTo(paths.Main);
                wallet.Should().BeEquivalentTo(paths.Wallet);
                cache.Should().BeEquivalentTo(paths.Cache);
            }
        }
    }
}