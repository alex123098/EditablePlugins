using System.Reflection;

namespace Editable.Host.Infrastructure
{
    public interface IPluginCompiler
    {
        Assembly CompileFrom(string sourceCode);

        void CopyReferencesConfiguration(Assembly assembly);
    }
}
