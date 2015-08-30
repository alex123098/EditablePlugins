using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace EditableControls
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", 
        Justification = "This is the composition root for WPF application.")]
    public partial class App
    {
        private Bootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _bootstrapper = new Bootstrapper();
            MainWindow = _bootstrapper.CreateMainWindow();
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _bootstrapper.Dispose();
            base.OnExit(e);
        }
    }
}
