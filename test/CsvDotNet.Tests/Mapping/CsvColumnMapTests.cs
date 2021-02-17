using CsvDotNet.Mapping;
using CsvDotNet.Tests.Samples;
using DotNetReflector;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CsvDotNet.Tests.Mapping
{
    public class CsvColumnMapTests
    {
        [Fact]
        public void When_column_index_is_equal_then_canmap_returns_true()
        {
            var reflector = Substitute.For<IPropertyReflector>();

            var map = new CsvColumnMap(0, reflector);

            map.CanMapIndex(0).Should().BeTrue();
        }

        [Fact]
        public void When_column_index_is_not_equal_then_canmap_returns_false()
        {
            var reflector = Substitute.For<IPropertyReflector>();

            var map = new CsvColumnMap(0, reflector);

            map.CanMapIndex(1).Should().BeFalse();
        }

        [Fact]
        public void When_appropriate_value_is_given_then_setvalue_is_called()
        {
            var reflector = Substitute.For<IPropertyReflector>();

            var map = new CsvColumnMap(0, reflector);

            var specimen = new NamedSampleRow();
            var value = "1";

            map.Map(specimen, value);

            reflector.Received(1).SetValue(specimen, value);
        }
    }
}
