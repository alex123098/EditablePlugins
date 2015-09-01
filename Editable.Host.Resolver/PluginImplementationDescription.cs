using System;

namespace Editable.Host.Resolver
{
    [Serializable]
    internal class PluginImplementationDescription
    {
        public string AssemblyName { get; }
        public string Abstraction { get; }
        public string Implementation { get; }

        public PluginImplementationDescription(string assemblyName, string abstraction, string implementation)
        {
            AssemblyName = assemblyName;
            Abstraction = abstraction;
            Implementation = implementation;
        }
    }
}