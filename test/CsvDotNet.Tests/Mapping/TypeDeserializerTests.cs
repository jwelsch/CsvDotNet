using AutoFixture;
using CsvDotNet.Mapping;
using CsvDotNet.Tests.Samples;
using FluentAssertions;
using System;
using Xunit;

namespace CsvDotNet.Tests.Mapping
{
    public class TypeDeserializerTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_deserializing_to_same_type_then_return_same_value()
        {
            var value = AutoFixture.Create<string>();

            var deserializer = new TypeDeserializer();

            var specimen = deserializer.Deserialize<string>(value);

            specimen.Should()
                    .BeOfType(typeof(string))
                    .And
                    .Be(value);
        }

        [Fact]
        public void When_deserializing_to_castable_type_then_return_equivalent_value()
        {
            var value = AutoFixture.Create<int>();

            var deserializer = new TypeDeserializer();

            var specimen = deserializer.Deserialize<int>(value.ToString());

            specimen.Should()
                    .BeOfType(typeof(int))
                    .And
                    .Be(value);
        }

        [Fact]
        public void When_deserializing_to_noncastable_type_then_throw_argumentexception()
        {
            var value = AutoFixture.Create<Guid>();

            var deserializer = new TypeDeserializer();

            Action specimen = () => deserializer.Deserialize<int>(value.ToString());

            specimen.Should()
                    .Throw<ArgumentException>();
        }

        [Fact]
        public void When_deserializing_empty_string_to_string_then_return_empty_string()
        {
            var value = string.Empty;

            var deserializer = new TypeDeserializer();

            var specimen = deserializer.Deserialize<string>(value);

            specimen.Should()
                    .BeOfType(typeof(string))
                    .And
                    .Be(value);
        }

        [Fact]
        public void When_deserializing_to_unsupported_type_then_throw_notsupportedexception()
        {
            var value = AutoFixture.Create<string>();

            var deserializer = new TypeDeserializer();

            Action specimen = () => deserializer.Deserialize<NamedSampleRow>(value);

            specimen.Should()
                    .Throw<NotSupportedException>();
        }

        [Fact]
        public void When_deserializing_null_to_string_then_return_empty_string()
        {
            var deserializer = new TypeDeserializer();

            var specimen = deserializer.Deserialize<string>(null);

            specimen.Should()
                    .BeNull();
        }
    }
}
