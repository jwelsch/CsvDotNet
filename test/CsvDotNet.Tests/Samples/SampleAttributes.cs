using System;

namespace CsvDotNet.Tests.Samples
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class NonCsvRowAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class NonCsvColumnAttribute : Attribute
    {
    }
}
