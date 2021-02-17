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

            if (value.GetType() == targetType.GetType())
            {
                return value;
            }

            //return (T)Convert.ChangeType(value, typeof(T));

            var converter = TypeDescriptor.GetConverter(targetType);
            return converter.ConvertFromString(value);
        }
    }
}
