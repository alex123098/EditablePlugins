using Editable.Host.CodeEditor.ViewModel;
using Editable.Host.Infrastructure;

namespace Editable.Host.CodeEditor.View
{
    internal interface ICodeEditorWindowFactory
    {
        IDialogWindow CreateDialogFor(ICodeEditorViewModel viewModel);
    }
}
