using System;

namespace CsvDotNet
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class NamedCsvRowAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class OrderedCsvRowAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class NamedCsvColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public bool CaseSensitive { get; set; } = true;
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class OrderedCsvColumnAttribute : Attribute
    {
        public int Order { get; }

        public OrderedCsvColumnAttribute(int order)
        {
            Order = order;
        }
    }
}
