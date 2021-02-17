using AutoFixture;
using DotNetReflector.Tests.Samples;
using FluentAssertions;
using Xunit;

namespace DotNetReflector.Tests
{
    public class MethodReflectorTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_return_type_is_the_same_then_equals_returns_true()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var info = type.GetMethod(nameof(sample.Method1));

            var reflector = new MethodReflector(info);

            reflector.ReturnType.Equals(new TypeReflector(info.ReturnType)).Should().BeTrue();
        }

        [Fact]
        public void When_return_type_is_not_the_same_then_equals_returns_false()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var info1 = type.GetMethod(nameof(sample.Method1));
            var info2 = type.GetMethod(nameof(sample.Method2));

            var reflector = new MethodReflector(info1);

            reflector.ReturnType.Equals(new TypeReflector(info2.ReturnType)).Should().BeFalse();
        }

        [Fact]
        public void When_getparameters_is_called_on_method_with_arguments_then_array_with_correct_elements_returned()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var info = type.GetMethod(nameof(sample.Method2));

            var reflector = new MethodReflector(info);

            var specimen = reflector.GetParameters();

            var paramReflector = new ParameterReflector(info.GetParameters()[0]);

            specimen.Should().HaveCount(1);

            specimen[0].Name.Should().Be(paramReflector.Name);
            specimen[0].ParameterType.Equals(paramReflector.ParameterType).Should().BeTrue();
        }

        [Fact]
        public void When_getparameters_is_called_on_method_with_no_arguments_then_return_empty_array()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var info = type.GetMethod(nameof(sample.Method1));

            var reflector = new MethodReflector(info);

            var specimen = reflector.GetParameters();

            specimen.Should().BeEmpty();
        }
    }
}
