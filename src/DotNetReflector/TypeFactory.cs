using System;

namespace DotNetReflector
{
    public interface ITypeFactory
    {
        T Create<T>();

        object Create(Type type, params object[] arguments);
    }

    public class TypeFactory : ITypeFactory
    {
        public T Create<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public object Create(Type type, params object[] arguments)
        {
            return Activator.CreateInstance(type, arguments);
        }
    }
}
