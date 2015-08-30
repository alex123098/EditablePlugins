using System;
using System.Windows.Input;
using Editable.Host.Mvvm;
using EditableControls.Command;
using JetBrains.Annotations;

namespace EditableControls.ViewModel
{
    [UsedImplicitly]
    internal class MainViewModel : NotifyPropertyChangedBase, IMainViewModel
    {
        public ICommand ExecuteVoidAction { get; }
        public ICommand ExecuteResultAction { get; }
        public ICommand EditVoidAction { get; }
        public ICommand EditResultAction { get; }

        public MainViewModel([NotNull] IMainCommandsFactory commandsFactory)
        {
            if (commandsFactory == null)
            {
                throw new ArgumentNullException(nameof(commandsFactory));
            }

            ExecuteVoidAction = commandsFactory.CreateExecuteVoidActionCommand();
            ExecuteResultAction = commandsFactory.CreateExecuteResultActionCommand();
            EditVoidAction = commandsFactory.CreateEditVoidActionCommand();
            EditResultAction = commandsFactory.CreateEditResultActionCommand();
        }
    }
}