using System;
using System.Windows;
using Editable.Host.Infrastructure;
using Editable.Host.Mvvm;
using JetBrains.Annotations;

namespace EditableControls.Command
{
    internal class ExecuteResultAction : CommandBase
    {
        private readonly IPluginFactory _pluginFactory;

        public ExecuteResultAction([NotNull] IPluginFactory pluginFactory)
        {
            if (pluginFactory == null)
            {
                throw new ArgumentNullException(nameof(pluginFactory));
            }
            _pluginFactory = pluginFactory;
        }

        public override void Execute(object parameter)
        {
            var plugin = _pluginFactory.CreateMethodPlugin<string>();
            var result = plugin.Invoke();
            MessageBox.Show($"Plugin returned result: {result}");
        }
    }
}
