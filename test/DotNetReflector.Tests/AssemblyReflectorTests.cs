using FluentAssertions;
using System.Reflection;
using Xunit;

namespace DotNetReflector.Tests
{
    public class AssemblyReflectorTests
    {
        [Fact]
        public void When_assembly_given_then_assembly_property_is_correct()
        {
            var asm = Assembly.GetExecutingAssembly();

            var reflector = new AssemblyReflector(asm);

            reflector.Assembly.Should().NotBeNull();
        }

        [Fact]
        public void When_assembly_given_then_fullname_property_is_correct()
        {
            var asm = Assembly.GetExecutingAssembly();

            var reflector = new AssemblyReflector(asm);

            reflector.FullName.Should().Be(asm.FullName);
        }

        [Fact]
        public void When_assembly_given_then_location_property_is_correct()
        {
            var asm = Assembly.GetExecutingAssembly();

            var reflector = new AssemblyReflector(asm);

            reflector.Location.Should().Be(asm.Location);
        }

        [Fact]
        public void When_assembly_given_then_location_getname_returns_assemblyname()
        {
            var asm = Assembly.GetExecutingAssembly();

            var reflector = new AssemblyReflector(asm);

            reflector.GetName().Should().NotBeNull();
        }
    }
}
