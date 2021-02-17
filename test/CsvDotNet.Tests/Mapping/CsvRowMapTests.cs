using AutoFixture;
using CsvDotNet.Mapping;
using CsvDotNet.Tests.Samples;
using DotNetReflector;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace CsvDotNet.Tests.Mapping
{
    public class CsvRowMapTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_named_csv_row_contains_valid_data_for_type_then_return_type()
        {
            var id = AutoFixture.Create<int>();
            var name = AutoFixture.Create<string>();
            var timestamp = AutoFixture.Create<DateTime>();

            var csvRow = new[]
            {
                id.ToString(),
                name,
                timestamp.ToString()
            };

            var sampleRow = new NamedSampleRow();

            var typeFactory = Substitute.For<ITypeFactory>();
            typeFactory.Create<NamedSampleRow>().Returns(sampleRow);

            var typeDeserializer = Substitute.For<ITypeDeserializer>();
            typeDeserializer.Deserialize(id.GetType(), csvRow[0]).Returns(id);
            typeDeserializer.Deserialize(name.GetType(), csvRow[1]).Returns(name);
            typeDeserializer.Deserialize(timestamp.GetType(), csvRow[2]).Returns(timestamp);

            var nameMap = Substitute.For<ICsvColumnMap>();
            nameMap.CanMapIndex(0).Returns(true);
            nameMap.Type.Returns(new TypeReflector(id.GetType()));

            var idMap = Substitute.For<ICsvColumnMap>();
            idMap.CanMapIndex(1).Returns(true);
            idMap.Type.Returns(new TypeReflector(name.GetType()));

            var timestampMap = Substitute.For<ICsvColumnMap>();
            timestampMap.CanMapIndex(2).Returns(true);
            timestampMap.Type.Returns(new TypeReflector(timestamp.GetType()));

            var maps = new[]
            {
                nameMap,
                idMap,
                timestampMap
            };

            var map = new CsvRowMap<NamedSampleRow>(typeFactory, typeDeserializer, maps);

            var row = map.Map(csvRow);

            row.GetType().Should().Be(typeof(NamedSampleRow));

            nameMap.Received(1).Map(sampleRow, id);
            idMap.Received(1).Map(sampleRow, name);
            timestampMap.Received(1).Map(sampleRow, timestamp);
        }

        [Fact]
        public void When_named_csv_row_does_not_contain_valid_data_for_type_then_throw_exception()
        {
            var id = AutoFixture.Create<Guid>();
            var name = AutoFixture.Create<string>();
            var timestamp = AutoFixture.Create<DateTime>();

            var csvRow = new[]
            {
                id.ToString(),
                name,
                timestamp.ToString()
            };

            var sampleRow = new NamedSampleRow();

            var typeFactory = Substitute.For<ITypeFactory>();
            typeFactory.Create<NamedSampleRow>().Returns(sampleRow);

            var typeDeserializer = Substitute.For<ITypeDeserializer>();
            typeDeserializer.Deserialize(id.GetType(), csvRow[0]).Returns(id);
            typeDeserializer.Deserialize(name.GetType(), csvRow[1]).Returns(name);
            typeDeserializer.Deserialize(timestamp.GetType(), csvRow[2]).Returns(timestamp);

            var nameMap = Substitute.For<ICsvColumnMap>();
            nameMap.CanMapIndex(0).Returns(false);
            nameMap.Type.Returns(new TypeReflector(id.GetType()));

            var idMap = Substitute.For<ICsvColumnMap>();
            idMap.CanMapIndex(1).Returns(true);
            idMap.Type.Returns(new TypeReflector(name.GetType()));

            var timestampMap = Substitute.For<ICsvColumnMap>();
            timestampMap.CanMapIndex(2).Returns(true);
            timestampMap.Type.Returns(new TypeReflector(timestamp.GetType()));

            var maps = new[]
            {
                nameMap,
                idMap,
                timestampMap
            };

            var map = new CsvRowMap<NamedSampleRow>(typeFactory, typeDeserializer, maps);

            Action specimen = () => map.Map(csvRow);

            specimen.Should().Throw<InvalidCastException>();
        }
    }
}
