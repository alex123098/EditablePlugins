using System;
using Editable.Host.Mvvm;
using JetBrains.Annotations;

namespace EditableControls.Command
{
    internal class EditVoidAction : CommandBase
    {
        private readonly PluginCompilationEngine _compilationEngine;

        public EditVoidAction([NotNull] PluginCompilationEngine compilationEngine)
        {
            if (compilationEngine == null)
            {
                throw new ArgumentNullException(nameof(compilationEngine));
            }
            _compilationEngine = compilationEngine;
        }

        public override void Execute(object parameter)
        {
            _compilationEngine.EditVoidPlugin();
        }
    }
}
