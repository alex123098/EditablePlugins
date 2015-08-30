using System.ComponentModel;
using System.Windows.Input;
using JetBrains.Annotations;

namespace Editable.Host.CodeEditor.ViewModel
{
    [UsedImplicitly]
    internal class DesignTimeCodeEditorViewModel : ICodeEditorViewModel
    {
        public string SourceCode { get; set; } = @"
namespace Plugins.Void 
{
    public class VoidMethodPlugin : IVoidMethodPlugin
    {
        public void Invoke()
        {
            // TODO: your implementation goes here.
        }
    }
}";

        public bool? DialogResult { get; set; } = null;
        public ICommand Save { get; } = null;

        public ICommand Cancel { get; } = null;
        public void CommitChange()
        {
            
        }

        public void RejectChange()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}