using System.Windows.Input;

namespace Editable.Host.CodeEditor.ViewModel
{
    interface ICodeEditorCommandsFactory
    {
        ICommand CreateSaveCommand();

        ICommand CreateCancelCommand();
    }
}
