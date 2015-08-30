namespace Editable.Host.Infrastructure
{
    public enum CommonDialogResult
    {
        Ok,
        Cancel
    }

    public interface IDialogWindow
    {
        CommonDialogResult Execute();
    }
}
