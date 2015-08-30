using Editable.Host.CodeEditor.View;
using Editable.Host.CodeEditor.ViewModel;
using Editable.Host.Infrastructure;
using Ninject.Modules;

namespace Editable.Host.CodeEditor
{
    public class ModuleRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<ICodeEditorService>().To<CodeEditorService>().InSingletonScope();
            Bind<ICodeEditorWindowFactory>().To<CodeEditorWindowFactory>().InSingletonScope();
            Bind<ICodeEditorViewModel>().To<CodeEditorViewModel>().InSingletonScope();
            Bind<ICodeEditorCommandsFactory>().To<CodeEditorCommandFactory>().InSingletonScope();
        }
    }
}
