using DotNetReflector;
using System;
using System.ComponentModel;

namespace CsvDotNet.Mapping
{
    public interface ITypeDeserializer
    {
        T Deserialize<T>(string value);

        object Deserialize(Type targetType, string value);
    }

    public class TypeDeserializer : ITypeDeserializer
    {
        private readonly ITypeFactory _typeFactory;

        public TypeDeserializer(ITypeFactory typeFactory)
        {
            _typeFactory = typeFactory;
        }

        public T Deserialize<T>(string value)
        {
            return (T)Deserialize(typeof(T), value);
        }

        public object Deserialize(Type targetType, string value)
        {
            if (value == null)
            {
                return null;
            }

            if (value.GetType() == targetType)
            {
                return value;
            }

            if (value.Length == 0)
            {
                return _typeFactory.Create(targetType);
            }

            var converter = TypeDescriptor.GetConverter(targetType);

            if (converter.IsValid(value))
            {
                return converter.ConvertFromString(value);
            }

            throw new CsvException($"Cannot deserialize value '{value}' to type '{targetType.FullName}'.");
        }
    }
}
