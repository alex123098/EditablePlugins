using System;
using Editable.Host.Infrastructure;
using Editable.Host.Mvvm;
using JetBrains.Annotations;

namespace EditableControls.Command
{
    internal class ExecuteVoidAction : CommandBase
    {
        private readonly IPluginFactory _pluginFactory;

        public ExecuteVoidAction([NotNull] IPluginFactory pluginFactory)
        {
            if (pluginFactory == null)
            {
                throw new ArgumentNullException(nameof(pluginFactory));
            }
            _pluginFactory = pluginFactory;
        }

        public override void Execute(object parameter)
        {
            var plugin = _pluginFactory.CreateVoidMethodPlugin();
            plugin.Invoke();
        }
    }
}
