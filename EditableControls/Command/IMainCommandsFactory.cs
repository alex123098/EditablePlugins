using System.Windows.Input;

namespace EditableControls.Command
{
    internal interface IMainCommandsFactory
    {
        ICommand CreateExecuteVoidActionCommand();

        ICommand CreateExecuteResultActionCommand();

        ICommand CreateEditVoidActionCommand();

        ICommand CreateEditResultActionCommand();
    }
}