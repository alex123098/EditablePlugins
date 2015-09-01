using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Editable.Host.Infrastructure;
using Editable.Infrastructure;

namespace Editable.Host.Resolver
{
    internal class PluginAssemblyDiscoverer
    {
        private class DiscovererProxy : MarshalByRefObject
        {
            public IReadOnlyCollection<PluginImplementationDescription> GetDeclaredMappings(byte[] assemblyBytes)
            {
                var assembly = Assembly.Load(assemblyBytes);
                var descriptionsQuery = from type in assembly.GetTypes()
                    where !type.IsAbstract && type.IsPublic
                    let pluginContractType =
                        type.GetInterfaces().FirstOrDefault(i => i.HasAttribute<PluginContractAttribute>())
                    where pluginContractType != null
                    select new PluginImplementationDescription(assembly.FullName, pluginContractType.FullName, type.FullName);
                return new ReadOnlyCollection<PluginImplementationDescription>(descriptionsQuery.ToList()); 
            }
        }

        public IReadOnlyCollection<PluginImplementationDescription> GetDeclaredMappings(byte[] assemblyBytes)
        {
            using (var keeper = new AppDomainKeeper("discovery"))
            {
                var remoteDiscoverer = (DiscovererProxy) keeper.Domain.CreateInstanceAndUnwrap(
                    typeof (PluginAssemblyDiscoverer).Assembly.FullName,
                    typeof (DiscovererProxy).FullName);

                return remoteDiscoverer.GetDeclaredMappings(assemblyBytes);
            }
        }
    }
}
