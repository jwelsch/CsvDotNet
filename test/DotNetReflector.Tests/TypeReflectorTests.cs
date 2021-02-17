using AutoFixture;
using DotNetReflector.Tests.Samples;
using FluentAssertions;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace DotNetReflector.Tests
{
    public class TypeReflectorTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_null_given_to_constructor_then_throw_argumentnullexception()
        {
            Action specimen = () => new TypeReflector(null);

            specimen.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void When_type_given_to_constructor_then_type_property_is_equal()
        {
            var sample = typeof(SampleClass);

            var specimen = new TypeReflector(sample);

            specimen.Type.Should().Be(sample);
        }

        [Fact]
        public void When_type_given_to_constructor_then_name_property_is_equal()
        {
            var sample = typeof(SampleClass);

            var specimen = new TypeReflector(sample);

            specimen.Name.Should().Be(sample.Name);
        }

        [Fact]
        public void When_type_given_to_constructor_then_fullname_property_is_equal()
        {
            var sample = typeof(SampleClass);

            var specimen = new TypeReflector(sample);

            specimen.FullName.Should().Be(sample.FullName);
        }

        [Fact]
        public void When_getmethod_called_with_existing_name_then_reflector_returned()
        {
            var sample = new SampleClass();
            var name = nameof(sample.Method1);

            var reflector = new TypeReflector(sample.GetType());

            var specimen = reflector.GetMethod(name);

            specimen.Name.Should().Be(name);
        }

        [Fact]
        public void When_getmethod_called_with_nonexisting_name_then_throw_notimplementedexception()
        {
            var sample = new SampleClass();
            var name = AutoFixture.Create<string>();

            var reflector = new TypeReflector(sample.GetType());

            Action specimen = () => reflector.GetMethod(name);

            specimen.Should().Throw<NotImplementedException>();
        }

        [Fact]
        public void When_getproperty_called_with_existing_name_then_reflector_returned()
        {
            var sample = new SampleClass();
            var name = nameof(sample.Property1);

            var reflector = new TypeReflector(sample.GetType());

            var specimen = reflector.GetProperty(name);

            specimen.Name.Should().Be(name);
        }

        [Fact]
        public void When_getproperty_called_with_nonexisting_name_then_throw_notimplementedexception()
        {
            var sample = new SampleClass();
            var name = AutoFixture.Create<string>();

            var reflector = new TypeReflector(sample.GetType());

            Action specimen = () => reflector.GetProperty(name);

            specimen.Should().Throw<NotImplementedException>();
        }

        [Fact]
        public void When_getfield_called_with_existing_name_then_reflector_returned()
        {
            var sample = new SampleClass();
            var name = nameof(sample.field1);

            var reflector = new TypeReflector(sample.GetType());

            var specimen = reflector.GetField(name);

            specimen.Name.Should().Be(name);
        }

        [Fact]
        public void When_getfield_called_with_nonexisting_name_then_throw_notimplementedexception()
        {
            var sample = new SampleClass();
            var name = AutoFixture.Create<string>();

            var reflector = new TypeReflector(sample.GetType());

            Action specimen = () => reflector.GetField(name);

            specimen.Should().Throw<NotImplementedException>();
        }

        [Fact]
        public void When_getmethods_called_then_reflectors_returned()
        {
            var sample = new SampleClass();

            var reflector = new TypeReflector(sample.GetType());

            var specimen = reflector.GetMethods();

            var expected = sample.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Select(i => new MethodReflector(i)).ToArray();

            specimen.Should().HaveCount(expected.Length);
        }

        [Fact]
        public void When_getproperties_called_then_reflectors_returned()
        {
            var sample = new SampleClass();

            var reflector = new TypeReflector(sample.GetType());

            var specimen = reflector.GetProperties();

            var expected = sample.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(i => new PropertyReflector(i)).ToArray();

            specimen.Should().HaveCount(expected.Length);
        }

        [Fact]
        public void When_getfields_called_then_reflectors_returned()
        {
            var sample = new SampleClass();

            var reflector = new TypeReflector(sample.GetType());

            var specimen = reflector.GetFields();

            var expected = sample.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).Select(i => new FieldReflector(i)).ToArray();

            specimen.Should().HaveCount(expected.Length);
        }

        [Fact]
        public void When_getcustomattributes_called_then_reflectors_returned()
        {
            var sample = new SampleClass();

            var reflector = new TypeReflector(sample.GetType());

            var specimen = reflector.GetCustomAttributes();

            var expected = sample.GetType().GetCustomAttributes().Select(i => new AttributeReflector(i)).ToArray();

            specimen.Should().HaveCount(expected.Length);
        }

        [Fact]
        public void When_equals_called_with_equivalent_type_reflector_then_return_true()
        {
            var sample1 = new SampleClass();
            var sample2 = new SampleClass();

            var reflector1 = new TypeReflector(sample1.GetType());
            var reflector2 = new TypeReflector(sample2.GetType());

            reflector1.Equals(reflector2).Should().BeTrue();
        }

        [Fact]
        public void When_equals_called_with_nonequivalent_type_reflector_then_return_false()
        {
            var sample1 = new SampleClass();
            var sample2 = new AnotherSampleClass();

            var reflector1 = new TypeReflector(sample1.GetType());
            var reflector2 = new TypeReflector(sample2.GetType());

            reflector1.Equals(reflector2).Should().BeFalse();
        }
    }
}
