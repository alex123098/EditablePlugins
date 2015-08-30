using Editable.Host.Infrastructure;
using Ninject.Modules;

namespace Editable.Host.InMemoryStorage
{
    public class ModuleRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IStrategiesSourceRepository>().To<InMemorySourceRepository>().InSingletonScope();
        }
    }
}
