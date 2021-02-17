using DotNetReflector;
using System;
using System.Collections.Generic;

namespace CsvDotNet.Mapping
{
    public interface ICsvRowMapFactory
    {
        ICsvRowMap<T> Create<T>(ICsvRowParser csvRowParser);
    }

    public class CsvRowMapFactory : ICsvRowMapFactory
    {
        private static readonly ITypeReflector _namedCsvRowAttributeType = new TypeReflector(typeof(NamedCsvRowAttribute));
        private static readonly ITypeReflector _namedCsvColumnAttributeType = new TypeReflector(typeof(NamedCsvColumnAttribute));
        private static readonly ITypeReflector _orderedCsvRowAttributeType = new TypeReflector(typeof(OrderedCsvRowAttribute));
        private static readonly ITypeReflector _orderedCsvColumnAttributeType = new TypeReflector(typeof(OrderedCsvColumnAttribute));

        private readonly ITypeFactory _typeFactory;
        private readonly ITypeReflectorFactory _typeReflectorFactory;
        private readonly ICsvColumnGetter _columnGetter;
        private readonly IOrdinalValidatorFactory _ordinalValidatorFactory;
        private readonly ITypeDeserializer _deserializer;

        public CsvRowMapFactory(ITypeFactory typeFactory, ITypeReflectorFactory typeReflectorFactory, ICsvColumnGetter columnGetter, IOrdinalValidatorFactory ordinalValidatorFactory, ITypeDeserializer deserializer)
        {
            _typeFactory = typeFactory;
            _typeReflectorFactory = typeReflectorFactory;
            _columnGetter = columnGetter;
            _ordinalValidatorFactory = ordinalValidatorFactory;
            _deserializer = deserializer;
        }

        public ICsvRowMap<T> Create<T>(ICsvRowParser csvRowParser)
        {
            var type = _typeReflectorFactory.Create<T>();
            var maps = new List<ICsvColumnMap>();

            var isNamed = IsNamedCsvColumns(type);

            if (isNamed)
            {
                var csvColumns = _columnGetter.GetColumns(type, _namedCsvColumnAttributeType);

                var csvRow = csvRowParser.GetNextRow();

                if (csvRow.Length == 0)
                {
                    return null;
                }

                foreach (var csvColumn in csvColumns)
                {
                    for (var i = 0; i < csvRow.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(csvRow[i]))
                        {
                            throw new InvalidOperationException($"The CSV header at index '{i}' is empty.");
                        }

                        if (csvColumn.Property.Name == csvRow[i])
                        {
                            maps.Add(new CsvColumnMap(i, csvColumn.Property));
                            break;
                        }
                    }
                }
            }
            else
            {
                var csvColumns = _columnGetter.GetColumns(type, _orderedCsvColumnAttributeType);

                var ordinalValidator = _ordinalValidatorFactory.Create(csvColumns.Length);

                foreach (var csvColumn in csvColumns)
                {
                    var order = csvColumn.Attribute.GetPropertyValue<int>("Order");

                    var status = ordinalValidator.Validate(order);

                    if (status == OrdinalStatus.InvalidAlreadyExists)
                    {
                        throw new InvalidOperationException($"The CSV order '{order}' already exists as a property attribute.");
                    }
                    else if (status == OrdinalStatus.InvalidUnexpected)
                    {
                        throw new InvalidOperationException($"The CSV order '{order}' is unexpected as a property attribute.");
                    }

                    maps.Add(new CsvColumnMap(order, csvColumn.Property));
                }
            }

            return new CsvRowMap<T>(_typeFactory, _deserializer, maps);
        }

        private static bool IsNamedCsvColumns(ITypeReflector typeReflector)
        {
            var attributes = typeReflector.GetCustomAttributes();

            foreach (var attribute in attributes)
            {
                if (attribute.Type.Equals(_namedCsvRowAttributeType))
                {
                    return true;
                }
                else if (attribute.Type.Equals(_orderedCsvRowAttributeType))
                {
                    return false;
                }
            }

            throw new ArgumentException($"No CSV row attributes were found decorating the type '{typeReflector.FullName}'.");
        }
    }
}