using DotNetReflector.Tests.Samples;
using FluentAssertions;
using System;
using System.Reflection;
using Xunit;

namespace DotNetReflector.Tests
{
    public class ParameterReflectorTests
    {
        [Fact]
        public void When_constructor_is_given_null_then_throw_argumentnullexception()
        {
            Action specimen = () => new MemberReflector<MemberInfo>(null);

            specimen.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void When_memberinfo_is_given_then_name_property_is_correct()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var methodInfo = type.GetMethod(nameof(sample.Method2));

            var paramInfo = methodInfo.GetParameters()[0];

            var specimen = new ParameterReflector(paramInfo);

            specimen.Name.Should().Be(paramInfo.Name);
        }

        [Fact]
        public void When_memberinfo_is_given_then_parametertype_property_type_is_correct()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var methodInfo = type.GetMethod(nameof(sample.Method2));

            var paramInfo = methodInfo.GetParameters()[0];

            var specimen = new ParameterReflector(paramInfo);

            specimen.ParameterType.Should().Be(new TypeReflector(paramInfo.ParameterType));
        }
    }
}
