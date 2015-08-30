using System.Windows.Input;
using Editable.Host.CodeEditor.Commands;
using JetBrains.Annotations;

namespace Editable.Host.CodeEditor.ViewModel
{
    [UsedImplicitly]
    internal class CodeEditorCommandFactory : ICodeEditorCommandsFactory
    {
        public ICommand CreateSaveCommand()
        {
            return new SaveCommand();
        }

        public ICommand CreateCancelCommand()
        {
            return new CancelCommand();
        }
    }
}
