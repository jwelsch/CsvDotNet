using AutoFixture;
using DotNetReflector.Tests.Samples;
using FluentAssertions;
using System;
using System.Reflection;
using Xunit;

namespace DotNetReflector.Tests
{
    public class PropertyReflectorTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_property_type_is_the_same_then_equals_returns_true()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var info = type.GetProperty(nameof(sample.Property1));

            var reflector = new PropertyReflector(info);

            reflector.PropertyType.Equals(new TypeReflector(sample.Property1.GetType())).Should().BeTrue();
        }

        [Fact]
        public void When_property_type_is_not_the_same_then_equals_returns_false()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var info = type.GetProperty(nameof(sample.Property1));

            var reflector = new PropertyReflector(info);

            reflector.PropertyType.Equals(new TypeReflector(sample.field2.GetType())).Should().BeFalse();
        }

        [Fact]
        public void When_getvalue_is_given_correct_object_then_return_value()
        {
            var value = AutoFixture.Create<int>();

            var sample = new SampleClass()
            {
                Property1 = value
            };

            var type = sample.GetType();
            var info = type.GetProperty(nameof(sample.Property1));

            var reflector = new PropertyReflector(info);

            var specimen = reflector.GetValue(sample);

            specimen.Should().Be(value);
        }

        [Fact]
        public void When_getvalue_is_given_incorrect_object_then_throw_argumentexception()
        {
            var value = AutoFixture.Create<int>();

            var sample1 = new SampleClass()
            {
                Property1 = value
            };

            var sample2 = "foo";

            var type = sample1.GetType();
            var info = type.GetProperty(nameof(sample1.Property1));

            var reflector = new PropertyReflector(info);

            Action specimen = () => reflector.GetValue(sample2);

            specimen.Should().Throw<TargetException>();
        }

        [Fact]
        public void When_setvalue_is_given_correct_object_and_correct_value_then_return_change_value()
        {
            var value1 = AutoFixture.Create<int>();
            var value2 = AutoFixture.Create<int>();

            var sample = new SampleClass()
            {
                Property1 = value1
            };

            var type = sample.GetType();
            var info = type.GetProperty(nameof(sample.Property1));

            var reflector = new PropertyReflector(info);

            reflector.SetValue(sample, value2);

            sample.Property1.Should().Be(value2);
        }

        [Fact]
        public void When_setvalue_is_given_correct_object_and_incorrect_value_then_return_change_value()
        {
            var value1 = AutoFixture.Create<int>();
            var value2 = AutoFixture.Create<string>();

            var sample = new SampleClass()
            {
                Property1 = value1
            };

            var type = sample.GetType();
            var info = type.GetProperty(nameof(sample.Property1));

            var reflector = new PropertyReflector(info);

            Action specimen = () => reflector.SetValue(sample, value2);

            specimen.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_setvalue_is_given_incorrect_object_and_correct_value_then_return_change_value()
        {
            var value1 = AutoFixture.Create<int>();
            var value2 = AutoFixture.Create<int>();

            var sample1 = new SampleClass()
            {
                Property1 = value1
            };

            var sample2 = "foo";

            var type = sample1.GetType();
            var info = type.GetProperty(nameof(sample1.Property1));

            var reflector = new PropertyReflector(info);

            Action specimen = () => reflector.SetValue(sample2, value2);

            specimen.Should().Throw<TargetException>();
        }
    }
}
