using System;

namespace Zenon
{
    public interface IEnvironment
    {
        string GetFolderPath(Environment.SpecialFolder folder);

        string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option);

        string GetEnvironmentVariable(string variable);

        string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);

        void SetEnvironmentVariable(string variable, string value);

        void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target);
    }

    public class DotNetEnvironment : IEnvironment
    {
        public string GetFolderPath(Environment.SpecialFolder folder)
        {
            return Environment.GetFolderPath(folder);
        }

        public string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return Environment.GetFolderPath(folder, option);
        }

        public string GetEnvironmentVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }

        public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
        {
            return Environment.GetEnvironmentVariable(variable, target);
        }

        public void SetEnvironmentVariable(string variable, string value)
        {
            Environment.SetEnvironmentVariable(variable, value);
        }

        public void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
        {
            Environment.SetEnvironmentVariable(variable, value, target);
        }
    }
}
