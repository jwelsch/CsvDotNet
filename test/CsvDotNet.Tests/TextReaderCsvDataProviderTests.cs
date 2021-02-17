using FluentAssertions;
using System.IO;
using Xunit;

namespace CsvDotNet.Tests
{
    public class TextReaderCsvDataProviderTests
    {
        [Fact]
        public void When_textreader_has_text_then_next_returns_character()
        {
            var reader = new StringReader("foobar");

            var provider = new TextReaderCsvDataProvider(reader);

            var specimen = provider.Next();

            specimen.Should().Be('f');
        }

        [Fact]
        public void When_textreader_has_text_then_multiple_peek_calls_return_the_same_character()
        {
            var reader = new StringReader("foobar");

            var provider = new TextReaderCsvDataProvider(reader);

            var specimen1 = provider.Peek();
            var specimen2 = provider.Peek();

            specimen1.Should().Be('f');
            specimen2.Should().Be('f');
        }

        [Fact]
        public void When_textreader_at_end_then_next_returns_negative_one()
        {
            var reader = new StringReader("f");

            var provider = new TextReaderCsvDataProvider(reader);

            var specimen1 = provider.Next();
            var specimen2 = provider.Next();

            specimen1.Should().Be('f');
            specimen2.Should().Be(-1);
        }

        [Fact]
        public void When_textreader_at_end_then_peek_returns_negative_one()
        {
            var reader = new StringReader("f");

            var provider = new TextReaderCsvDataProvider(reader);

            var specimen1 = provider.Next();
            var specimen2 = provider.Peek();

            specimen1.Should().Be('f');
            specimen2.Should().Be(-1);
        }
    }
}
