using AutoFixture;
using DotNetReflector.Tests.Samples;
using FluentAssertions;
using System;
using Xunit;

namespace DotNetReflector.Tests
{
    public class AttributeReflectorTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_attribute_type_is_the_same_then_equals_returns_true()
        {
            var attribute1 = new SampleAttribute();
            var attribute2 = new SampleAttribute();

            var expected = new AttributeReflector(attribute2);

            var specimen = new AttributeReflector(attribute1);

            specimen.Equals(expected).Should().BeTrue();
        }

        [Fact]
        public void When_attribute_type_is_not_the_same_then_equals_returns_false()
        {
            var attribute1 = new SampleAttribute();
            var attribute2 = new AnotherSampleAttribute();

            var notExpected = new AttributeReflector(attribute2);

            var specimen = new AttributeReflector(attribute1);

            specimen.Equals(notExpected).Should().BeFalse();
        }

        [Fact]
        public void When_getpropertyvalue_is_called_with_existing_property_name_then_correct_value_is_returned()
        {
            var value = AutoFixture.Create<string>();

            var attribute = new SampleAttribute
            {
                Name = value
            };

            var reflector = new AttributeReflector(attribute);

            var specimen = reflector.GetPropertyValue<string>(nameof(attribute.Name));

            specimen.Should().Be(value);
        }

        [Fact]
        public void When_getpropertyvalue_is_called_with_nonexisting_property_name_then_notimplementedexception_is_thrown()
        {
            var name = AutoFixture.Create<string>();

            var attribute = new SampleAttribute();

            var reflector = new AttributeReflector(attribute);

            Action specimen = () => reflector.GetPropertyValue<string>(name);

            specimen.Should().Throw<NotImplementedException>();
        }

        [Fact]
        public void When_getpropertyvalue_is_called_with_existing_property_name_but_wrong_type_then_invalidcastexception_is_thrown()
        {
            var attribute = new SampleAttribute();

            var reflector = new AttributeReflector(attribute);

            Action specimen = () => reflector.GetPropertyValue<string>(nameof(attribute.Id));

            specimen.Should().Throw<InvalidCastException>();
        }
    }
}
