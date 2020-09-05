namespace Catman.Blogger.API.Exceptions
{
    using System;

    internal class EnvironmentVariableDoesNotExist : Exception
    {
        private static string GetMessage(string variableName)
        {
            return $"Environment variable {variableName} does not exist";
        }

        public EnvironmentVariableDoesNotExist(string variableName, Exception innerException = default)
            : base(GetMessage(variableName), innerException)
        {
        }
    }
}
