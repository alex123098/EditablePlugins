using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;

namespace Editable.Host.Compilation
{
    [UsedImplicitly]
    internal class AssemblyReferenceProvider : IAssemblyReferenceProvider
    {
        public IEnumerable<MetadataReference> CollectMetadataReferences(Assembly sourceReferencesAssembly)
        {
            var references = sourceReferencesAssembly.GetReferencedAssemblies();
            return references
                .Select(GetAssembly)
                .Select(asm => MetadataReference.CreateFromFile(asm.Location));
        }

        private Assembly GetAssembly(AssemblyName assemblyName)
        {
            if (assemblyName.CodeBase == null)
            {
                return Assembly.ReflectionOnlyLoad(assemblyName.FullName);
            }
            return Assembly.ReflectionOnlyLoadFrom(assemblyName.CodeBase);
        }
    }
}
