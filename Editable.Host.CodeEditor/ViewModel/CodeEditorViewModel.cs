using System;
using System.Windows.Input;
using Editable.Host.Mvvm;
using JetBrains.Annotations;

namespace Editable.Host.CodeEditor.ViewModel
{
    [UsedImplicitly]
    internal class CodeEditorViewModel : NotifyPropertyChangedBase, ICodeEditorViewModel
    {
        private string _sourceCode;
        private bool? _dialogResult;

        public string SourceCode
        {
            get { return _sourceCode; }
            set { UpdateProperty(ref _sourceCode, value); }
        }

        public bool? DialogResult
        {
            get { return _dialogResult; }
            set { UpdateProperty(ref _dialogResult, value); }
        }

        public ICommand Save { get; }

        public ICommand Cancel { get; }

        public CodeEditorViewModel(ICodeEditorCommandsFactory commandsFactory)
        {
            if (commandsFactory == null)
            {
                throw new ArgumentNullException(nameof(commandsFactory));
            }
            Save = commandsFactory.CreateSaveCommand();
            Cancel = commandsFactory.CreateCancelCommand();
        }

        public void CommitChange()
        {
            DialogResult = true;
        }

        public void RejectChange()
        {
            DialogResult = false;
        }
    }
}
