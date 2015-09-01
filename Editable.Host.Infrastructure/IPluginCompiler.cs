using System.Reflection;

namespace Editable.Host.Infrastructure
{
    public interface IPluginCompiler
    {
        byte[] CompileFrom(string sourceCode);

        void CopyReferencesConfiguration(Assembly assembly);
    }
}
