using System;
using Editable.Host.Infrastructure;
using JetBrains.Annotations;

namespace Editable.Host.InMemoryStorage
{
    [UsedImplicitly]
    internal class InMemorySourceRepository : IStrategiesSourceRepository
    {
        private string _voidMethodPluginSource;
        private string _resultMethodPluginSource;

        public InMemorySourceRepository()
        {
            _voidMethodPluginSource = 
@"using Editable.Infrastructure;
namespace Plugins.Void 
{
    public class VoidMethodPlugin : IVoidMethodPlugin
    {
        public void Invoke()
        {
            // TODO: your implementation goes here.
        }
    }
}";
            _resultMethodPluginSource =
@"using Editable.Infrastructure;
namespace Plugins.Typed
{
    public class ObjectMethodPlugin : IMethodPlugin<object> 
    {
        public object Invoke()
        {
            // TODO: your implementation goes here.
            return null;
        }
    }
}";
        }

        public string FetchVoidMethodPluginSource() => _voidMethodPluginSource;

        public string FetchResultMethodPluginSource() => _resultMethodPluginSource;

        public void StoreVoidMethodPluginSource(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            _voidMethodPluginSource = source;
        }

        public void StoreResultMethodPluginSource(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            _resultMethodPluginSource = source;
        }
    }
}
