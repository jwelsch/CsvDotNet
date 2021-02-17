using FluentAssertions;
using System;
using System.IO;
using Xunit;

namespace CsvDotNet.Tests
{
    public class CsvRowParserTests
    {
        [Fact]
        public void When_getnextrow_called_before_initialize_then_throws()
        {
            var reader = new StringReader(string.Empty);
            var provider = new TextReaderCsvDataProvider(reader);

            var specimen = new CsvRowParser();

            Action act = () => specimen.GetNextRow();

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_csv_data_empty_getnextrow_returns_array_with_zero_elements()
        {
            var reader = new StringReader(string.Empty);
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen = parser.GetNextRow();

            specimen.Length.Should().Be(0);
        }

        [Fact]
        public void When_csv_data_is_single_column_getnextrow_returns_array_with_one_element()
        {
            var reader = new StringReader("foo");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen = parser.GetNextRow();

            specimen.Length.Should().Be(1);
            specimen[0].Should().Be("foo");
        }

        [Fact]
        public void When_csv_data_is_two_column_then_getnextrow_returns_array_with_two_elements()
        {
            var reader = new StringReader("foo,bar");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen = parser.GetNextRow();

            specimen.Length.Should().Be(2);
            specimen[0].Should().Be("foo");
            specimen[1].Should().Be("bar");
        }

        [Fact]
        public void When_csv_row_has_no_trailing_newline_then_getnextrow_returns_correct_rows()
        {
            var reader = new StringReader($"foo,bar");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(2);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(0);
        }

        [Fact]
        public void When_csv_row_is_crlf_then_getnextrow_returns_correct_rows()
        {
            var newLine = "\r\n";
            var reader = new StringReader($"foo,bar{newLine}");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(2);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(0);
        }

        [Fact]
        public void When_csv_row_is_cr_then_getnextrow_returns_correct_rows()
        {
            var newLine = "\r";
            var reader = new StringReader($"foo,bar{newLine}");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(2);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(0);
        }

        [Fact]
        public void When_csv_row_is_lf_then_getnextrow_returns_correct_rows()
        {
            var newLine = "\n";
            var reader = new StringReader($"foo,bar{newLine}");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(2);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(0);
        }

        [Fact]
        public void When_multiple_trailing_newlines_then_getnextrow_returns_correct_rows()
        {
            var newLine = "\r\n";
            var reader = new StringReader($"foo,bar{newLine}{newLine}");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(2);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(1);

            var specimen3 = parser.GetNextRow();

            specimen3.Length.Should().Be(0);
        }

        [Fact]
        public void When_csv_data_is_crlf_then_getnextrow_returns_correct_rows()
        {
            var newLine = "\r\n";
            var reader = new StringReader($"foo,bar{newLine}too,baz");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(2);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(2);
            specimen2[0].Should().Be("too");
            specimen2[1].Should().Be("baz");
        }

        [Fact]
        public void When_csv_data_is_cr_then_getnextrow_returns_correct_rows()
        {
            var newLine = "\r";
            var reader = new StringReader($"foo,bar{newLine}too,baz");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(2);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(2);
            specimen2[0].Should().Be("too");
            specimen2[1].Should().Be("baz");
        }

        [Fact]
        public void When_csv_data_is_lf_then_getnextrow_returns_correct_rows()
        {
            var newLine = "\n";
            var reader = new StringReader($"foo,bar{newLine}too,baz");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(2);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(2);
            specimen2[0].Should().Be("too");
            specimen2[1].Should().Be("baz");
        }

        [Fact]
        public void When_first_column_is_empty_then_getnextrow_returns_array_with_two_elements()
        {
            var reader = new StringReader(",foo,bar");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen = parser.GetNextRow();

            specimen.Length.Should().Be(3);
            specimen[0].Should().Be(string.Empty);
            specimen[1].Should().Be("foo");
            specimen[2].Should().Be("bar");
        }

        [Fact]
        public void When_second_column_is_empty_then_getnextrow_returns_array_with_two_elements()
        {
            var reader = new StringReader("foo,,bar");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen = parser.GetNextRow();

            specimen.Length.Should().Be(3);
            specimen[0].Should().Be("foo");
            specimen[1].Should().Be(string.Empty);
            specimen[2].Should().Be("bar");
        }

        [Fact]
        public void When_last_column_is_empty_then_getnextrow_returns_array_with_two_elements()
        {
            var reader = new StringReader("foo,bar,");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen = parser.GetNextRow();

            specimen.Length.Should().Be(3);
            specimen[0].Should().Be("foo");
            specimen[1].Should().Be("bar");
            specimen[2].Should().Be(string.Empty);
        }

        [Fact]
        public void When_last_column_is_empty_and_has_trailing_newline_then_getnextrow_returns_array_with_two_elements()
        {
            var reader = new StringReader("foo,bar,\r\n");
            var provider = new TextReaderCsvDataProvider(reader);

            var parser = new CsvRowParser();
            parser.Initialize(provider);

            var specimen1 = parser.GetNextRow();

            specimen1.Length.Should().Be(3);
            specimen1[0].Should().Be("foo");
            specimen1[1].Should().Be("bar");
            specimen1[2].Should().Be(string.Empty);

            var specimen2 = parser.GetNextRow();

            specimen2.Length.Should().Be(0);
        }
    }
}
