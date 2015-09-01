using System;

namespace Editable.Host.Infrastructure
{
    [Serializable]
    public class ClassLocationInfo
    {
        public string AssemblyName { get; }
        public string TypeName { get; }
        public byte[] AssemblyBytes { get; }
        public string[] GenericTypeParameters { get; set; }

        public ClassLocationInfo(string assemblyName, string typeName, byte[] assemblyBytes)
        {
            TypeName = typeName;
            AssemblyBytes = assemblyBytes;
            AssemblyName = assemblyName;
        }
    }
}