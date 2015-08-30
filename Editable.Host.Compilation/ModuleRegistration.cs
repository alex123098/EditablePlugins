using Editable.Host.Infrastructure;
using Ninject.Modules;

namespace Editable.Host.Compilation
{
    public class ModuleRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IPluginCompiler>().To<PluginCompiler>().InSingletonScope();
            Bind<IAssemblyReferenceProvider>().To<AssemblyReferenceProvider>().InSingletonScope();
        }
    }
}
