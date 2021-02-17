using System;

namespace DotNetReflector
{
    public interface ITypeReflectorFactory
    {
        ITypeReflector Create<T>();

        ITypeReflector Create(Type type);
    }

    public class TypeReflectorFactory : ITypeReflectorFactory
    {
        public ITypeReflector Create<T>()
        {
            return Create(typeof(T));
        }

        public ITypeReflector Create(Type type)
        {
            return new TypeReflector(type);
        }
    }
}
