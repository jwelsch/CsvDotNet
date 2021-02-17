using CsvDotNet.Tests.Samples;
using FluentAssertions;
using System.IO;
using System.Text;
using Xunit;

namespace CsvDotNet.Tests
{
    public class CsvConverterTests
    {
        [Fact]
        public void When_totype_is_called_with_textreader_then_expected_types_are_returned()
        {
            var specimen = CsvConverter.ToType<NamedSampleRow>(new StringReader("Id,Name,Timestamp\r\n1,Foo,2021-01-01T00:00:01"));

            specimen.GetType().Should().Be<NamedSampleRow[]>();
        }

        [Fact]
        public void When_totype_is_called_with_stream_then_expected_types_are_returned()
        {
            var buffer = Encoding.UTF8.GetBytes("Id,Name,Timestamp\r\n1,Foo,2021-01-01T00:00:01");

            using var stream = new MemoryStream(buffer);

            var specimen = CsvConverter.ToType<NamedSampleRow>(stream);

            specimen.GetType().Should().Be<NamedSampleRow[]>();
        }
    }
}
