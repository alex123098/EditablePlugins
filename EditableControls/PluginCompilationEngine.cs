using System;
using System.Windows;
using Editable.Host.Infrastructure;
using JetBrains.Annotations;

namespace EditableControls
{
    [UsedImplicitly]
    internal class PluginCompilationEngine
    {
        private readonly IStrategiesSourceRepository _pluginSourceRepository;
        private readonly ICodeEditorService _codeEditor;
        private readonly IPluginCompiler _pluginCompiler;
        private readonly IPluginLocator _pluginLocator;

        public PluginCompilationEngine(
            [NotNull] IStrategiesSourceRepository pluginSourceRepository,
            [NotNull] ICodeEditorService codeEditor,
            [NotNull] IPluginCompiler pluginCompiler,
            [NotNull] IPluginLocator pluginLocator)
        {
            if (pluginSourceRepository == null)
            {
                throw new ArgumentNullException(nameof(pluginSourceRepository));
            }
            if (codeEditor == null)
            {
                throw new ArgumentNullException(nameof(codeEditor));
            }
            if (pluginCompiler == null)
            {
                throw new ArgumentNullException(nameof(pluginCompiler));
            }
            if (pluginLocator == null)
            {
                throw new ArgumentNullException(nameof(pluginLocator));
            }

            _pluginSourceRepository = pluginSourceRepository;
            _codeEditor = codeEditor;
            _pluginCompiler = pluginCompiler;
            _pluginLocator = pluginLocator;
        }

        public void EditVoidPlugin()
        {
            var initialSource = _pluginSourceRepository.FetchVoidMethodPluginSource();
            var resultSource = _codeEditor.EditPluginCode(initialSource);
            _pluginSourceRepository.StoreVoidMethodPluginSource(resultSource);
            CompileAndApplyPlugin(resultSource);
        }

        private void CompileAndApplyPlugin(string resultSource)
        {
            try
            {
                var assembly = _pluginCompiler.CompileFrom(resultSource);
                _pluginLocator.RegisterAssembly(assembly);
            }
            catch (PluginCompilationException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void EditResultPlugin()
        {
            var initialSource = _pluginSourceRepository.FetchResultMethodPluginSource();
            var resultSource = _codeEditor.EditPluginCode(initialSource);
            _pluginSourceRepository.StoreResultMethodPluginSource(resultSource);
            CompileAndApplyPlugin(resultSource);
        }
    }
}