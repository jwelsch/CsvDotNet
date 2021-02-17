using CsvDotNet.Mapping;
using CsvDotNet.Tests.Samples;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace CsvDotNet.Tests
{
    public class CsvRowConverterTests
    {
        [Fact]
        public void When_csv_data_has_zero_rows_then_return_empty_array()
        {
            var parser = Substitute.For<ICsvRowParser>();
            parser.GetNextRow().Returns(new string[0]);

            var rowMapFactory = Substitute.For<ICsvRowMapFactory>();
            var dataProvider = Substitute.For<ICsvDataProvider>();

            var converter = new CsvRowConverter(parser, rowMapFactory);

            var specimen = converter.ToType<NamedSampleRow>(dataProvider);

            specimen.Should().BeEmpty();
        }

        [Fact]
        public void When_csv_data_is_named_and_has_only_header_row_then_return_empty_array()
        {
            var row = new NamedSampleRow()
            {
                Id = 1,
                Name = "Foo",
                Timestamp = DateTime.Parse("2021-02-15")
            };

            var parser = Substitute.For<ICsvRowParser>();
            parser.GetNextRow().Returns(new string[0]);

            var rowMap = Substitute.For<ICsvRowMap<NamedSampleRow>>();
            rowMap.Map(Arg.Any<string[]>()).Returns(row);

            var rowMapFactory = Substitute.For<ICsvRowMapFactory>();
            rowMapFactory.Create<NamedSampleRow>(Arg.Any<ICsvRowParser>()).Returns(rowMap);

            var dataProvider = Substitute.For<ICsvDataProvider>();

            var converter = new CsvRowConverter(parser, rowMapFactory);

            var specimen = converter.ToType<NamedSampleRow>(dataProvider);

            specimen.Should().BeEmpty();
        }

        [Fact]
        public void When_csv_data_is_named_and_has_header_row_and_one_data_row_then_return_array_with_one_element()
        {
            var row = new NamedSampleRow()
            {
                Id = 1,
                Name = "Foo",
                Timestamp = DateTime.Parse("2021-02-15")
            };

            var parser = Substitute.For<ICsvRowParser>();
            parser.GetNextRow().Returns(new[] { row.Id.ToString(), row.Name, row.Timestamp.ToString() }, new string[0]);

            var rowMap = Substitute.For<ICsvRowMap<NamedSampleRow>>();
            rowMap.Map(Arg.Any<string[]>()).Returns(row);

            var rowMapFactory = Substitute.For<ICsvRowMapFactory>();
            rowMapFactory.Create<NamedSampleRow>(Arg.Any<ICsvRowParser>()).Returns(rowMap);

            var dataProvider = Substitute.For<ICsvDataProvider>();

            var converter = new CsvRowConverter(parser, rowMapFactory);

            var specimen = converter.ToType<NamedSampleRow>(dataProvider);

            specimen.Should().ContainSingle();
            specimen.Should().ContainInOrder(row);
        }

        [Fact]
        public void When_csv_data_is_named_and_has_header_row_and_multiple_data_rows_then_return_array_with_multiple_elements()
        {
            var row0 = new NamedSampleRow()
            {
                Id = 1,
                Name = "Foo",
                Timestamp = DateTime.Parse("2021-02-15")
            };

            var row1 = new NamedSampleRow()
            {
                Id = 2,
                Name = "Bar",
                Timestamp = DateTime.Parse("2021-02-16")
            };

            var parser = Substitute.For<ICsvRowParser>();
            parser.GetNextRow().Returns(new[] { row0.Id.ToString(), row0.Name, row0.Timestamp.ToString() }, new[] { row1.Id.ToString(), row1.Name, row1.Timestamp.ToString() }, new string[0]);

            var rowMap = Substitute.For<ICsvRowMap<NamedSampleRow>>();
            rowMap.Map(Arg.Any<string[]>()).Returns(row0, row1);

            var rowMapFactory = Substitute.For<ICsvRowMapFactory>();
            rowMapFactory.Create<NamedSampleRow>(Arg.Any<ICsvRowParser>()).Returns(rowMap);

            var dataProvider = Substitute.For<ICsvDataProvider>();

            var converter = new CsvRowConverter(parser, rowMapFactory);

            var specimen = converter.ToType<NamedSampleRow>(dataProvider);

            specimen.Should().ContainInOrder(row0, row1);
        }
    }
}
