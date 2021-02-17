using AutoFixture;
using CsvDotNet.Mapping;
using FluentAssertions;
using Xunit;

namespace CsvDotNet.Tests.Mapping
{
    public class OrdinalTrackerTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_ctor_is_given_ordinal_then_ordinal_property_returns_ordinal()
        {
            var ordinal = AutoFixture.Create<int>();

            var specimen = new OrdinalTracker(ordinal);

            specimen.Ordinal.Should().Be(ordinal);
        }
    }
}
