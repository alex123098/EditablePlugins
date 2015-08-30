using System;
using Editable.Host.CodeEditor.View;
using Editable.Host.CodeEditor.ViewModel;
using Editable.Host.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.CodeEditor
{
    [UsedImplicitly]
    internal class CodeEditorService : ICodeEditorService
    {
        private readonly ICodeEditorViewModel _codeEditorViewModel;
        private readonly ICodeEditorWindowFactory _windowFactory;

        public CodeEditorService(
            [NotNull] ICodeEditorViewModel codeEditorViewModel,
            [NotNull] ICodeEditorWindowFactory windowFactory)
        {
            if (codeEditorViewModel == null)
            {
                throw new ArgumentNullException(nameof(codeEditorViewModel));
            }
            if (windowFactory == null)
            {
                throw new ArgumentNullException(nameof(windowFactory));
            }
            _codeEditorViewModel = codeEditorViewModel;
            _windowFactory = windowFactory;
        }

        public string EditPluginCode(string initialSource)
        {
            _codeEditorViewModel.SourceCode = initialSource;
            _codeEditorViewModel.DialogResult = null;
            var dialog = _windowFactory.CreateDialogFor(_codeEditorViewModel);
            if (dialog.Execute() != CommonDialogResult.Ok)
            {
                return initialSource;
            }
            return _codeEditorViewModel.SourceCode;
        }
    }
}
