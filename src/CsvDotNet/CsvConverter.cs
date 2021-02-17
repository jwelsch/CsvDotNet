using Autofac;
using CsvDotNet.Mapping;
using System.IO;

namespace CsvDotNet
{
    public static class CsvConverter
    {
        private static IContainer Container { get; } = Registrator.Register();

        public static T[] ToType<T>(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                return ToType<T>(fileStream);
            }
        }

        public static T[] ToType<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return ToType<T>(reader);
            }
        }

        public static T[] ToType<T>(TextReader reader)
        {
            var rowParser = Container.Resolve<ICsvRowParser>();
            var rowMapFactory = Container.Resolve<ICsvRowMapFactory>();

            return ToType<T>(reader, rowParser, rowMapFactory);
        }

        public static T[] ToType<T>(TextReader reader, ICsvRowParser rowParser, ICsvRowMapFactory rowMapFactory)
        {
            var converter = new CsvRowConverter(rowParser, rowMapFactory);

            return converter.ToType<T>(new TextReaderCsvDataProvider(reader));
        }
    }
}
