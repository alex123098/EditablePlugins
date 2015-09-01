using Editable.Host.Infrastructure;
using Editable.Host.Resolver.Marshaling;
using Ninject.Modules;

namespace Editable.Host.Resolver
{
    public class ModuleRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IProxyTypeLocator>().To<ProxyTypeLocator>().InSingletonScope();
            Bind<IPluginLocator>().To<SingleImplementationPluginLocator>().InSingletonScope();
            Bind<IPluginFactory>().To<PluginFactory>().InSingletonScope();
        }
    }
}
