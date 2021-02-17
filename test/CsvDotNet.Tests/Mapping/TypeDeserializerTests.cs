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
    public class TypeDeserializerTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_deserializing_to_same_type_then_return_same_value()
        {
            var value = AutoFixture.Create<string>();

            var typeFactory = Substitute.For<ITypeFactory>();

            var deserializer = new TypeDeserializer(typeFactory);

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

            var typeFactory = Substitute.For<ITypeFactory>();

            var deserializer = new TypeDeserializer(typeFactory);

            var specimen = deserializer.Deserialize<int>(value.ToString());

            specimen.Should()
                    .BeOfType(typeof(int))
                    .And
                    .Be(value);
        }

        [Fact]
        public void When_deserializing_to_noncastable_type_then_throw_exception()
        {
            var value = AutoFixture.Create<Guid>();

            var typeFactory = Substitute.For<ITypeFactory>();

            var deserializer = new TypeDeserializer(typeFactory);

            Action specimen = () => deserializer.Deserialize<int>(value.ToString());

            specimen.Should()
                    .Throw<CsvException>();
        }

        [Fact]
        public void When_deserializing_empty_string_to_string_then_return_empty_string()
        {
            var value = string.Empty;

            var typeFactory = Substitute.For<ITypeFactory>();

            var deserializer = new TypeDeserializer(typeFactory);

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

            var typeFactory = Substitute.For<ITypeFactory>();

            var deserializer = new TypeDeserializer(typeFactory);

            Action specimen = () => deserializer.Deserialize<NamedSampleRow>(value);

            specimen.Should()
                    .Throw<CsvException>();
        }

        [Fact]
        public void When_deserializing_null_to_string_then_return_null()
        {
            var typeFactory = Substitute.For<ITypeFactory>();

            var deserializer = new TypeDeserializer(typeFactory);

            var specimen = deserializer.Deserialize<string>(null);

            specimen.Should()
                    .BeNull();
        }

        [Fact]
        public void When_deserializing_empty_string_to_int_then_return_default()
        {
            var type = typeof(int);

            var typeFactory = Substitute.For<ITypeFactory>();
            typeFactory.Create(type).Returns(0);

            var deserializer = new TypeDeserializer(typeFactory);

            var specimen = deserializer.Deserialize<int>(string.Empty);

            typeFactory.Received(1).Create(type);
            specimen.Should()
                    .Be(default);
        }

        [Fact]
        public void When_deserializing_null_to_int_then_return_throw_exception()
        {
            var deserializer = new TypeDeserializer(new TypeFactory());

            Action specimen = () => deserializer.Deserialize<int>(null);

            specimen.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void When_deserializing_empty_string_to_datetime_then_return_default()
        {
            var type = typeof(DateTime);

            var typeFactory = Substitute.For<ITypeFactory>();
            typeFactory.Create(type).Returns(default(DateTime));

            var deserializer = new TypeDeserializer(typeFactory);

            var specimen = deserializer.Deserialize<DateTime>(string.Empty);

            typeFactory.Received(1).Create(type);
            specimen.Should()
                    .Be(default);
        }

        [Fact]
        public void When_deserializing_null_to_datetime_then_return_throw_exception()
        {
            var deserializer = new TypeDeserializer(new TypeFactory());

            Action specimen = () => deserializer.Deserialize<DateTime>(null);

            specimen.Should().Throw<NullReferenceException>();
        }
    }
}
