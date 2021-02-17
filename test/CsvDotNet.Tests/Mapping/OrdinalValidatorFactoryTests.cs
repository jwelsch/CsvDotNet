using CsvDotNet.Mapping;
using FluentAssertions;
using System;
using Xunit;

namespace CsvDotNet.Tests.Mapping
{
    public class OrdinalValidatorFactoryTests
    {
        [Fact]
        public void When_given_negative_then_throw_exception()
        {
            var factory = new OrdinalValidatorFactory();

            Action specimen = () => factory.Create(-1);

            specimen.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_given_zero_then_return_empty_validator()
        {
            var factory = new OrdinalValidatorFactory();

            var validator = factory.Create(0);

            var specimen = validator.GetUnvalidatedOrdinals();

            specimen.Should().BeEmpty();
        }

        [Fact]
        public void When_given_greater_than_zero_then_return_validator_with_same_number_of_ordinals()
        {
            var factory = new OrdinalValidatorFactory();

            var count = 10;

            var validator = factory.Create(count);

            var specimen = validator.GetUnvalidatedOrdinals();

            specimen.Should().HaveCount(count);
        }
    }
}
