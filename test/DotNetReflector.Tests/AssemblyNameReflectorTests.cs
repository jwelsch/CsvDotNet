using FluentAssertions;
using System.Reflection;
using Xunit;

namespace DotNetReflector.Tests
{
    public class AssemblyNameReflectorTests
    {
        [Fact]
        public void When_assemblyname_given_then_assemblyname_property_is_correct()
        {
            var asmName = Assembly.GetExecutingAssembly().GetName();

            var reflector = new AssemblyNameReflector(asmName);

            reflector.AssemblyName.Should().Be(asmName);
        }

        [Fact]
        public void When_assemblyname_given_then_name_property_is_correct()
        {
            var asmName = Assembly.GetExecutingAssembly().GetName();

            var reflector = new AssemblyNameReflector(asmName);

            reflector.Name.Should().Be(asmName.Name);
        }

        [Fact]
        public void When_assemblyname_given_then_fullname_property_is_correct()
        {
            var asmName = Assembly.GetExecutingAssembly().GetName();

            var reflector = new AssemblyNameReflector(asmName);

            reflector.FullName.Should().Be(asmName.FullName);
        }

        [Fact]
        public void When_assemblyname_given_then_version_property_is_correct()
        {
            var asmName = Assembly.GetExecutingAssembly().GetName();

            var reflector = new AssemblyNameReflector(asmName);

            reflector.Version.Should().Be(asmName.Version);
        }
    }
}
