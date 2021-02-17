using DotNetReflector;
using System.Collections.Generic;
using System.Reflection;

namespace CsvDotNet
{
    public interface ICsvColumn
    {
        IPropertyReflector Property { get; }

        IAttributeReflector Attribute { get; }
    }

    public class CsvColumn : ICsvColumn
    {
        public IPropertyReflector Property { get; }

        public IAttributeReflector Attribute { get; }

        public CsvColumn(IPropertyReflector property, IAttributeReflector attribute)
        {
            Property = property;
            Attribute = attribute;
        }
    }

    public interface ICsvColumnGetter
    {
        ICsvColumn[] GetColumns(ITypeReflector type, ITypeReflector columnAttributeType);
    }

    public class CsvColumnGetter : ICsvColumnGetter
    {
        public ICsvColumn[] GetColumns(ITypeReflector type, ITypeReflector columnAttributeType)
        {
            var csvProperties = new List<ICsvColumn>();

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes();

                foreach (var attribute in attributes)
                {
                    if (attribute.Type.Equals(columnAttributeType))
                    {
                        csvProperties.Add(new CsvColumn(property, attribute));
                        break;
                    }
                }
            }

            return csvProperties.ToArray();
        }
    }
}
