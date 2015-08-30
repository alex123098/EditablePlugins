using System;
using System.Runtime.Serialization;

namespace Editable.Host.Infrastructure
{
    [Serializable]
    public class PluginCompilationException : Exception
    {
        public PluginCompilationException(string message)
            : base(message)
        { }

        public PluginCompilationException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected PluginCompilationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        { }
    }
}
