using Autofac;
using CsvDotNet.Mapping;
using DotNetReflector;

namespace CsvDotNet
{
    internal static class Registrator
    {
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();

            // Reflection
            builder.RegisterType<TypeDeserializer>().As<ITypeDeserializer>();
            builder.RegisterType<TypeReflectorFactory>().As<ITypeReflectorFactory>();
            builder.RegisterType<TypeFactory>().As<ITypeFactory>();

            // CSV Parsing
            builder.RegisterType<CsvRowParser>().As<ICsvRowParser>();
            builder.RegisterType<CsvRowConverter>().As<ICsvRowConverter>();
            builder.RegisterType<CsvColumnGetter>().As<ICsvColumnGetter>();
            builder.RegisterType<OrdinalValidatorFactory>().As<IOrdinalValidatorFactory>();
            builder.RegisterType<CsvRowMapFactory>().As<ICsvRowMapFactory>();

            return builder.Build();
        }
    }
}
