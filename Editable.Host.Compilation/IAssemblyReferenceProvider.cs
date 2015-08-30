using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Editable.Host.Compilation
{
    internal interface IAssemblyReferenceProvider
    {
        IEnumerable<MetadataReference> CollectMetadataReferences(Assembly sourceReferencesAssembly);
    }
}