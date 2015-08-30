using System;
using System.Reflection;

namespace Editable.Host.Infrastructure
{
    public interface IPluginLocator
    {
        void RegisterAssembly(Assembly assembly);

        Type LocateImplementation(Type pluginType);
    }
}
