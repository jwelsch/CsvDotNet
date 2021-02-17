using System.Linq;
using System.Reflection;

namespace DotNetReflector
{
    public interface IAssemblyReflector
    {
        string FullName { get; }

        string Location { get; }

        IAssemblyNameReflector GetName();

        ITypeReflector[] GetTypes();

        object CreateInstance(string typeName);

        T CreateInstance<T>();
    }

    public class AssemblyReflector : IAssemblyReflector
    {
        public Assembly Assembly { get; }

        private IAssemblyNameReflector _assemblyNameWrap;
        private ITypeReflector[] _typeWraps;

        public string FullName => Assembly.FullName;

        public string Location => Assembly.Location;

        public AssemblyReflector(Assembly assembly)
        {
            Assembly = assembly;
        }

        public IAssemblyNameReflector GetName()
        {
            if (_assemblyNameWrap == null)
            {
                _assemblyNameWrap = new AssemblyNameReflector(Assembly.GetName());
            }

            return _assemblyNameWrap;
        }

        public ITypeReflector[] GetTypes()
        {
            if (_typeWraps == null)
            {
                _typeWraps = Assembly.GetTypes().Select(i => new TypeReflector(i)).ToArray();
            }

            return _typeWraps;
        }

        public object CreateInstance(string typeName)
        {
            return Assembly.CreateInstance(typeName);
        }

        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T).FullName);
        }
    }
}
