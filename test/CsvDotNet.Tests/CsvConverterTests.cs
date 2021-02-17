using CsvDotNet.Tests.Samples;
using FluentAssertions;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace CsvDotNet.Tests
{
    public class CsvConverterTests
    {
        [Fact]
        public void When_totype_is_called_with_textreader_with_named_csv_data_then_expected_types_are_returned()
        {
            var specimen = CsvConverter.ToType<NamedSampleRow>(new StringReader("Id,Name,Timestamp\r\n1,Foo,2021-01-01T00:00:01\r\n2,Bar,2021-01-01T00:00:02"));

            var expected1 = new NamedSampleRow() { Id = 1, Name = "Foo", Timestamp = DateTime.Parse("2021-01-01T00:00:01"), Count = 0 };
            var expected2 = new NamedSampleRow() { Id = 2, Name = "Bar", Timestamp = DateTime.Parse("2021-01-01T00:00:02"), Count = 0 };

            specimen.GetType().Should().Be<NamedSampleRow[]>();
            specimen.Should()
                    .HaveCount(2)
                    .And
                    .ContainInOrder(expected1, expected2);
        }

        [Fact]
        public void When_totype_is_called_with_stream_with_named_csv_data_then_expected_types_are_returned()
        {
            var buffer = Encoding.UTF8.GetBytes("Id,Name,Timestamp\r\n1,Foo,2021-01-01T00:00:01\r\n2,Bar,2021-01-01T00:00:02");

            using var stream = new MemoryStream(buffer);

            var specimen = CsvConverter.ToType<NamedSampleRow>(stream);

            var expected1 = new NamedSampleRow() { Id = 1, Name = "Foo", Timestamp = DateTime.Parse("2021-01-01T00:00:01"), Count = 0 };
            var expected2 = new NamedSampleRow() { Id = 2, Name = "Bar", Timestamp = DateTime.Parse("2021-01-01T00:00:02"), Count = 0 };

            specimen.GetType().Should().Be<NamedSampleRow[]>();
            specimen.Should()
                    .HaveCount(2)
                    .And
                    .ContainInOrder(expected1, expected2);
        }

        [Fact]
        public void When_totype_is_called_with_textreader_with_ordered_csv_data_then_expected_types_are_returned()
        {
            var specimen = CsvConverter.ToType<OrderedSampleRow>(new StringReader("1,Foo,2021-01-01T00:00:01\r\n2,Bar,2021-01-01T00:00:02"));

            var expected1 = new OrderedSampleRow() { Id = 1, Name = "Foo", Timestamp = DateTime.Parse("2021-01-01T00:00:01"), Count = 0 };
            var expected2 = new OrderedSampleRow() { Id = 2, Name = "Bar", Timestamp = DateTime.Parse("2021-01-01T00:00:02"), Count = 0 };

            specimen.GetType().Should().Be<OrderedSampleRow[]>();
            specimen.Should()
                    .HaveCount(2)
                    .And
                    .ContainInOrder(expected1, expected2);
        }

        [Fact]
        public void When_totype_is_called_with_stream_with_ordered_csv_data_then_expected_types_are_returned()
        {
            var buffer = Encoding.UTF8.GetBytes("1,Foo,2021-01-01T00:00:01\r\n2,Bar,2021-01-01T00:00:02");

            using var stream = new MemoryStream(buffer);

            var specimen = CsvConverter.ToType<OrderedSampleRow>(stream);

            var expected1 = new OrderedSampleRow() { Id = 1, Name = "Foo", Timestamp = DateTime.Parse("2021-01-01T00:00:01"), Count = 0 };
            var expected2 = new OrderedSampleRow() { Id = 2, Name = "Bar", Timestamp = DateTime.Parse("2021-01-01T00:00:02"), Count = 0 };

            specimen.GetType().Should().Be<OrderedSampleRow[]>();
            specimen.Should()
                    .HaveCount(2)
                    .And
                    .ContainInOrder(expected1, expected2);
        }
    }
}
