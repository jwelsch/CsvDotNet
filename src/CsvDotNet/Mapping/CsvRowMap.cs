using DotNetReflector;
using System;

namespace CsvDotNet.Mapping
{
    public interface ICsvRowMap<T>
    {
        int ColumnCount { get; }

        T Map(string[] csvRow);
    }

    public class CsvRowMap<T> : ICsvRowMap<T>
    {
        private readonly ITypeFactory _typeFactory;
        private readonly ITypeDeserializer _deserializer;
        private readonly ICsvColumnMap[] _maps;

        public int ColumnCount => _maps.Length;

        public CsvRowMap(ITypeFactory typeFactory, ITypeDeserializer deserializer, ICsvColumnMap[] maps)
        {
            _typeFactory = typeFactory;
            _deserializer = deserializer;
            _maps = maps;
        }

        public T Map(string[] csvRow)
        {
            var instance = _typeFactory.Create<T>();

            for (var i = 0; i < csvRow.Length; i++)
            {
                var wasMapped = false;

                foreach (var map in _maps)
                {
                    if (map.CanMapIndex(i))
                    {
                        try
                        {
                            var value = _deserializer.Deserialize(map.Type.Type, csvRow[i]);
                            map.Map(instance, value);
                            wasMapped = true;
                            break;
                        }
                        catch (InvalidCastException ex)
                        {
                            throw new InvalidCastException($"Cannot map value '{csvRow[i]}' to column type '{map.Type.FullName}' in row '{i}'.", ex);
                        }
                        catch (NotSupportedException ex)
                        {
                            throw new InvalidCastException($"Cannot map value '{csvRow[i]}' to column type '{map.Type.FullName}' in row '{i}'.", ex);
                        }
                    }
                }

                if (!wasMapped)
                {
                    throw new InvalidCastException($"No map found for CSV value in column '{i}'.");
                }
            }

            return instance;
        }
    }
}
