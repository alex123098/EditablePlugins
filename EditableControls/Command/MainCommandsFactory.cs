using System;
using System.Windows.Input;
using Editable.Host.Infrastructure;
using JetBrains.Annotations;

namespace EditableControls.Command
{
    [UsedImplicitly]
    internal class MainCommandsFactory : IMainCommandsFactory
    {
        private readonly IPluginFactory _pluginFactory;
        private readonly PluginCompilationEngine _compilationEngine;

        public MainCommandsFactory(
            [NotNull] IPluginFactory pluginFactory,
            [NotNull] PluginCompilationEngine compilationEngine)
        {
            if (pluginFactory == null)
            {
                throw new ArgumentNullException(nameof(pluginFactory));
            }
            if (compilationEngine == null)
            {
                throw new ArgumentNullException(nameof(compilationEngine));
            }
            _pluginFactory = pluginFactory;
            _compilationEngine = compilationEngine;
        }

        public ICommand CreateExecuteVoidActionCommand()
        {
            return new ExecuteVoidAction(_pluginFactory);
        }

        public ICommand CreateExecuteResultActionCommand()
        {
            return new ExecuteResultAction(_pluginFactory);
        }

        public ICommand CreateEditVoidActionCommand()
        {
            return new EditVoidAction(_compilationEngine);
        }

        public ICommand CreateEditResultActionCommand()
        {
            return new EditResultAction(_compilationEngine);
        }
    }
}