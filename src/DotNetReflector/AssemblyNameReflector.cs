using System;
using System.Reflection;

namespace DotNetReflector
{
    public interface IAssemblyNameReflector
    {
        string Name { get; }

        string FullName { get; }

        Version Version { get; }
    }

    public class AssemblyNameReflector : IAssemblyNameReflector
    {
        public AssemblyName AssemblyName { get; }

        public string Name => AssemblyName.Name;

        public string FullName => AssemblyName.FullName;

        public Version Version => AssemblyName.Version;

        public AssemblyNameReflector(AssemblyName assemblyName)
        {
            AssemblyName = assemblyName;
        }
    }
}
