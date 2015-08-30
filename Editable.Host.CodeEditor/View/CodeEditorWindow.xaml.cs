using System.ComponentModel;
using Editable.Host.CodeEditor.ViewModel;
using Editable.Host.Infrastructure;

namespace Editable.Host.CodeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class CodeEditorWindow : IDialogWindow
    {
        private readonly ICodeEditorViewModel _viewModel;

        public CodeEditorWindow(ICodeEditorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.DialogResult) && _viewModel.DialogResult.HasValue)
            {
                DialogResult = _viewModel.DialogResult;
                _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
                Close();
            }
        }

        public CommonDialogResult Execute()
        {
            return ShowDialog() == true ? CommonDialogResult.Ok : CommonDialogResult.Cancel;
        }
    }
}
