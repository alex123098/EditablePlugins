using System;
using Editable.Host.Infrastructure;
using EditableControls.Command;
using EditableControls.ViewModel;
using Ninject;

namespace EditableControls
{
    internal sealed class Bootstrapper : IDisposable
    {
        private readonly IKernel _kernel;
        public Bootstrapper()
        {
            _kernel = new StandardKernel();
            WireUpModules();
            BindLocalServices();
            SetAssemblyDependencies();
        }

        private void SetAssemblyDependencies()
        {
            _kernel.Get<IPluginCompiler>().CopyReferencesConfiguration(typeof(Bootstrapper).Assembly);
        }

        private void BindLocalServices()
        {
            _kernel.Bind<IMainViewModel>().To<MainViewModel>();
            _kernel.Bind<IMainCommandsFactory>().To<MainCommandsFactory>().InSingletonScope();
            _kernel.Bind<PluginCompilationEngine>().ToSelf().InSingletonScope();
        }

        private void WireUpModules()
        {
            _kernel.Load<Editable.Host.CodeEditor.ModuleRegistration>();
            _kernel.Load<Editable.Host.Compilation.ModuleRegistration>();
            _kernel.Load<Editable.Host.InMemoryStorage.ModuleRegistration>();
            _kernel.Load<Editable.Host.Resolver.ModuleRegistration>();
        }

        public MainWindow CreateMainWindow()
        {
            return _kernel.Get<MainWindow>();
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}
