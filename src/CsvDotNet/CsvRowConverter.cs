using CsvDotNet.Mapping;
using System.Collections.Generic;

namespace CsvDotNet
{
    public interface ICsvRowConverter
    {
        T[] ToType<T>(ICsvDataProvider dataProvider);
    }

    public class CsvRowConverter : ICsvRowConverter
    {
        private readonly ICsvRowParser _parser;
        private readonly ICsvRowMapFactory _csvRowMapFactory;

        public CsvRowConverter(ICsvRowParser parser, ICsvRowMapFactory csvRowMapFactory)
        {
            _parser = parser;
            _csvRowMapFactory = csvRowMapFactory;
        }

        public T[] ToType<T>(ICsvDataProvider dataProvider)
        {
            var result = new List<T>();
            var loop = true;

            _parser.Initialize(dataProvider);

            var rowMap = _csvRowMapFactory.Create<T>(_parser);

            while (loop)
            {
                var csvRow = _parser.GetNextRow();

                if (csvRow.Length == 0)
                {
                    loop = false;
                    continue;
                }

                var rowObj = rowMap.Map(csvRow);

                result.Add(rowObj);
            }

            return result.ToArray();
        }
    }
}
