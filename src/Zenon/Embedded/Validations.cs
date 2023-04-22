using System;
using System.Linq;

namespace Zenon.Embedded
{
    public class Validations
    {
        public static string TokenName(string value)
        {
            if (value != null)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return "Token name cannot be empty";
                }
                if (!Constants.TokenNameRegExp.IsMatch(value))
                {
                    return "Token name must contain only alphanumeric characters";
                }
                if (value.Length > Constants.TokenNameMaxLength)
                {
                    return $"Token name must have maximum {Constants.TokenNameMaxLength} characters";
                }
                return null;
            }
            else
            {
                return "Value is null";
            }
        }

        public static string TokenSymbol(string value)
        {
            if (value != null)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return "Token symbol cannot be empty";
                }
                if (!Constants.TokenSymbolRegExp.IsMatch(value))
                {
                    return "Token symbol must match pattern: ${tokenSymbolRegExp.pattern}";
                }
                if (value.Length > Constants.TokenSymbolMaxLength)
                {
                    return $"Token symbol must have maximum {Constants.TokenSymbolMaxLength} characters";
                }
                if (Constants.TokenSymbolExceptions.Contains(value))
                {
                    return $"Token symbol must not be one of the following: {String.Join(",", Constants.TokenSymbolExceptions)}";
                }
                return null;
            }
            else
            {
                return "Value is null";
            }
        }

        public static string TokenDomain(string value)
        {
            if (value != null)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return "Token domain cannot be empty";
                }
                if (!Constants.TokenDomainRegExp.IsMatch(value))
                {
                    return "Domain is not valid";
                }
                return null;
            }
            else
            {
                return "Value is null";
            }
        }

        public static string PillarName(string value)
        {
            if (value != null)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return "Pillar name cannot be empty";
                }
                if (!Constants.PillarNameRegExp.IsMatch(value))
                {
                    return "Pillar name must match pattern : ${pillarNameRegExp.pattern}";
                }
                if (value.Length > Constants.PillarNameMaxLength)
                {
                    return $"Pillar name must have maximum {Constants.PillarNameMaxLength} characters";
                }
                return null;
            }
            else
            {
                return "Value is null";
            }
        }

        public static string ProjectName(string value)
        {
            if (value != null)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return "Project name cannot be empty";
                }
                if (value.Length > Constants.ProjectNameMaxLength)
                {
                    return $"Project name must have maximum {Constants.ProjectNameMaxLength} characters";
                }
                return null;
            }
            else
            {
                return "Value is null";
            }
        }

        public static string ProjectDescription(string value)
        {
            if (value != null)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return "Project description cannot be empty";
                }
                if (value.Length > Constants.ProjectDescriptionMaxLength)
                {
                    return $"Project description must have maximum {Constants.ProjectDescriptionMaxLength} characters";
                }
                return null;
            }
            else
            {
                return "Value is null";
            }
        }
    }
}
