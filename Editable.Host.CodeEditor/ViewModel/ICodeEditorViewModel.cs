using System.ComponentModel;
using System.Windows.Input;

namespace Editable.Host.CodeEditor.ViewModel
{
    internal interface ICodeEditorViewModel : INotifyPropertyChanged
    {
        string SourceCode { get; set; }
        bool? DialogResult { get; set; }
        ICommand Save { get; }
        ICommand Cancel { get; }
        void CommitChange();
        void RejectChange();
    }
}