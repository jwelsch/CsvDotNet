using DotNetReflector.Tests.Samples;
using FluentAssertions;
using System;
using System.Reflection;
using Xunit;

namespace DotNetReflector.Tests
{
    public class MemberReflectorTests
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
            var info = type.GetProperty(nameof(sample.Property1));

            var specimen = new MemberReflector<PropertyInfo>(info);

            specimen.Name.Should().Be(nameof(SampleClass.Property1));
        }

        [Fact]
        public void When_memberinfo_is_given_then_memberinfo_property_type_is_correct()
        {
            var sample = new SampleClass();

            var type = sample.GetType();
            var info = type.GetProperty(nameof(sample.Property1));

            var specimen = new MemberReflector<PropertyInfo>(info);

            specimen.MemberInfo.GetType().Should().Be(info.GetType());
        }
    }
}
