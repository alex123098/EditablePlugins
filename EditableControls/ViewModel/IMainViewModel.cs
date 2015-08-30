using System.Windows.Input;
using JetBrains.Annotations;

namespace EditableControls.ViewModel
{
    public interface IMainViewModel
    {
        ICommand ExecuteVoidAction { get; }
        ICommand ExecuteResultAction { get; }
        ICommand EditVoidAction { get; }
        ICommand EditResultAction { get; }
    }

    [UsedImplicitly]
    internal class DesignTimeMainViewModel : IMainViewModel
    {
        public ICommand ExecuteVoidAction { get; } = null;
        public ICommand ExecuteResultAction { get; } = null;
        public ICommand EditVoidAction { get; } = null;
        public ICommand EditResultAction { get; } = null;
    }
}
