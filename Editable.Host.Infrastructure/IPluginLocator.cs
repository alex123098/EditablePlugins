using System;

namespace Editable.Host.Infrastructure
{
    public interface IPluginLocator
    {
        void RegisterAssembly(byte[] assemblyBytes);

        ClassLocationInfo LocateImplementation(Type pluginType);
    }
}
