using Editable.Host.CodeEditor.ViewModel;
using Editable.Host.Mvvm;

namespace Editable.Host.CodeEditor.Commands
{
    internal class SaveCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as ICodeEditorViewModel;
            viewModel?.CommitChange();
        }
    }
}