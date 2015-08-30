using Editable.Host.CodeEditor.ViewModel;
using Editable.Host.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.CodeEditor.View
{
    [UsedImplicitly]
    internal class CodeEditorWindowFactory : ICodeEditorWindowFactory
    {
        public IDialogWindow CreateDialogFor(ICodeEditorViewModel viewModel)
        {
            return new CodeEditorWindow(viewModel);
        }
    }
}
