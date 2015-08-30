using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Editable.Host.Infrastructure;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace Editable.Host.Compilation
{
    [UsedImplicitly]
    internal class PluginCompiler : IPluginCompiler
    {
        private readonly IAssemblyReferenceProvider _assemblyReferenceProvider;
        private Assembly _sourceReferencesAssembly;

        public PluginCompiler([NotNull] IAssemblyReferenceProvider assemblyReferenceProvider)
        {
            if (assemblyReferenceProvider == null)
            {
                throw new ArgumentNullException(nameof(assemblyReferenceProvider));
            }
            _assemblyReferenceProvider = assemblyReferenceProvider;
        }

        public void CopyReferencesConfiguration([NotNull] Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            _sourceReferencesAssembly = assembly;
        }

        public Assembly CompileFrom(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                throw new ArgumentException("Source code was not provided.", nameof(sourceCode));
            }

            var references = _assemblyReferenceProvider.CollectMetadataReferences(_sourceReferencesAssembly);
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            var compilation = CSharpCompilation.Create(
                Guid.NewGuid().ToString("N"),
                syntaxTree.AsEnumerable(),
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var outputStream = new MemoryStream())
            {
                var compilationResult = compilation.Emit(outputStream);
                VerifyCompilationResults(compilationResult);
                outputStream.Seek(0, SeekOrigin.Begin);
                return Assembly.Load(outputStream.ToArray());
            }
        }

        private void VerifyCompilationResults(EmitResult compilationResult)
        {
            if (compilationResult.Success)
            {
                return;
            }
            var errorsString = string.Join(
                Environment.NewLine,
                compilationResult.Diagnostics
                    .Where(diagnostic => diagnostic.IsWarningAsError ||
                                         diagnostic.Severity == DiagnosticSeverity.Error)
                    .Select(diagnostic => $"{diagnostic.Id}: {diagnostic.GetMessage(CultureInfo.CurrentUICulture)}"));
            throw new PluginCompilationException(errorsString);
        }
    }
}
