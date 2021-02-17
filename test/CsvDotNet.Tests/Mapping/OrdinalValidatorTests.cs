using CsvDotNet.Mapping;
using FluentAssertions;
using System;
using Xunit;

namespace CsvDotNet.Tests.Mapping
{
    public class OrdinalValidatorTests
    {
        [Fact]
        public void When_ordinal_is_tracked_and_is_validated_then_return_valid()
        {
            var ordinals = new[] { 1, 2, 3 };

            var validator = new OrdinalValidator(ordinals);

            var specimen = validator.Validate(1);

            specimen.Should().Be(OrdinalStatus.Valid);
        }

        [Fact]
        public void When_ordinal_is_not_tracked_and_is_validated_then_return_invalid()
        {
            var ordinals = new[] { 1, 2, 3 };

            var validator = new OrdinalValidator(ordinals);

            var specimen = validator.Validate(4);

            specimen.Should().Be(OrdinalStatus.InvalidUnexpected);
        }

        [Fact]
        public void When_ordinal_is_tracked_and_is_validated_more_than_once_then_return_invalid()
        {
            var ordinals = new[] { 1, 2, 3 };

            var validator = new OrdinalValidator(ordinals);

            validator.Validate(3);

            var specimen = validator.Validate(3);

            specimen.Should().Be(OrdinalStatus.InvalidAlreadyExists);
        }

        [Fact]
        public void When_ordinal_appears_more_than_once_then_throw_exception()
        {
            var ordinals = new[] { 1, 2, 2 };

            Action specimen = () => new OrdinalValidator(ordinals);

            specimen.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_all_ordinals_are_validated_then_return_empty_array()
        {
            var ordinals = new[] { 1, 2, 3 };

            var validator = new OrdinalValidator(ordinals);

            validator.Validate(1);
            validator.Validate(2);
            validator.Validate(3);

            var specimen = validator.GetUnvalidatedOrdinals();

            specimen.Should().BeEmpty();
        }

        [Fact]
        public void When_some_ordinals_are_validated_then_return_array_with_unvalidated_ordinals()
        {
            var ordinals = new[] { 1, 2, 3 };

            var validator = new OrdinalValidator(ordinals);

            validator.Validate(1);
            validator.Validate(3);

            var specimen = validator.GetUnvalidatedOrdinals();

            specimen.Should().HaveCount(1);
            specimen[0].Should().Be(2);
        }

        [Fact]
        public void When_ctor_is_given_empty_array_then_return_invalid()
        {
            var ordinals = new int[0];

            var validator = new OrdinalValidator(ordinals);

            var specimen = validator.Validate(1);

            specimen.Should().Be(OrdinalStatus.InvalidUnexpected);
        }

        [Fact]
        public void When_ctor_is_given_null_then_return_throw_exception()
        {
            Action specimen = () => new OrdinalValidator(null);

            specimen.Should().Throw<ArgumentNullException>();
        }
    }
}
