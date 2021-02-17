using DotNetReflector;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CsvDotNet.Tests
{
    public class CsvColumnGetterTests
    {
        [Fact]
        public void When_type_has_no_properties_then_return_empty_array()
        {
            var type = Substitute.For<ITypeReflector>();
            type.GetProperties().Returns(new IPropertyReflector[0]);

            var attributeType = Substitute.For<ITypeReflector>();

            var getter = new CsvColumnGetter();

            var specimen = getter.GetColumns(type, attributeType);

            specimen.Should().BeEmpty();
        }

        [Fact]
        public void When_type_has_no_properties_with_attributes_then_return_empty_array()
        {
            var propertyTypes = new[]
            {
                Substitute.For<IPropertyReflector>()
            };

            propertyTypes[0].GetCustomAttributes().Returns(new IAttributeReflector[0]);

            var type = Substitute.For<ITypeReflector>();
            type.GetProperties().Returns(propertyTypes);

            var attributeType = Substitute.For<ITypeReflector>();

            var getter = new CsvColumnGetter();

            var specimen = getter.GetColumns(type, attributeType);

            specimen.Should().BeEmpty();
        }

        [Fact]
        public void When_type_has_no_properties_with_matching_attributes_then_return_empty_array()
        {
            var attributeUnderlyingType = Substitute.For<ITypeReflector>();
            attributeUnderlyingType.Equals(Arg.Any<ITypeReflector>()).Returns(false);

            var attributeTypes = new[]
            {
                Substitute.For<IAttributeReflector>()
            };
            attributeTypes[0].Type.Returns(attributeUnderlyingType);

            var propertyTypes = new[]
            {
                Substitute.For<IPropertyReflector>()
            };
            propertyTypes[0].GetCustomAttributes().Returns(attributeTypes);

            var type = Substitute.For<ITypeReflector>();
            type.GetProperties().Returns(propertyTypes);

            var attributeType = Substitute.For<ITypeReflector>();

            var getter = new CsvColumnGetter();

            var specimen = getter.GetColumns(type, attributeType);

            specimen.Should().BeEmpty();
        }

        [Fact]
        public void When_type_has_one_property_with_matching_attribute_then_return_array_with_one_element()
        {
            var attributeUnderlyingType = Substitute.For<ITypeReflector>();
            attributeUnderlyingType.Equals(Arg.Any<ITypeReflector>()).Returns(true);

            var attributeTypes = new[]
            {
                Substitute.For<IAttributeReflector>()
            };
            attributeTypes[0].Type.Returns(attributeUnderlyingType);

            var propertyTypes = new[]
            {
                Substitute.For<IPropertyReflector>()
            };
            propertyTypes[0].GetCustomAttributes().Returns(attributeTypes);

            var type = Substitute.For<ITypeReflector>();
            type.GetProperties().Returns(propertyTypes);

            var attributeType = Substitute.For<ITypeReflector>();

            var getter = new CsvColumnGetter();

            var specimen = getter.GetColumns(type, attributeType);

            specimen.Should().HaveCount(1);
            specimen[0].Attribute.Should().Be(attributeTypes[0]);
            specimen[0].Property.Should().Be(propertyTypes[0]);
        }

        [Fact]
        public void When_type_has_multiple_properties_where_one_with_matching_attribute_then_return_array_with_one_element()
        {
            var attributeUnderlyingType0 = Substitute.For<ITypeReflector>();
            attributeUnderlyingType0.Equals(Arg.Any<ITypeReflector>()).Returns(true);

            var attributeUnderlyingType1 = Substitute.For<ITypeReflector>();
            attributeUnderlyingType1.Equals(Arg.Any<ITypeReflector>()).Returns(false);

            var attributeTypes0 = new[]
            {
                Substitute.For<IAttributeReflector>()
            };
            attributeTypes0[0].Type.Returns(attributeUnderlyingType0);

            var attributeTypes1 = new[]
            {
                Substitute.For<IAttributeReflector>()
            };
            attributeTypes1[0].Type.Returns(attributeUnderlyingType1);

            var propertyTypes = new[]
            {
                Substitute.For<IPropertyReflector>(),
                Substitute.For<IPropertyReflector>()
            };
            propertyTypes[0].GetCustomAttributes().Returns(attributeTypes0);
            propertyTypes[1].GetCustomAttributes().Returns(attributeTypes1);

            var type = Substitute.For<ITypeReflector>();
            type.GetProperties().Returns(propertyTypes);

            var attributeType = Substitute.For<ITypeReflector>();

            var getter = new CsvColumnGetter();

            var specimen = getter.GetColumns(type, attributeType);

            specimen.Should().HaveCount(1);
            specimen[0].Attribute.Should().Be(attributeTypes0[0]);
            specimen[0].Property.Should().Be(propertyTypes[0]);
        }

        [Fact]
        public void When_type_has_multiple_properties_all_with_matching_attribute_then_return_array_with_expected_elements()
        {
            var attributeUnderlyingType0 = Substitute.For<ITypeReflector>();
            attributeUnderlyingType0.Equals(Arg.Any<ITypeReflector>()).Returns(true);

            var attributeUnderlyingType1 = Substitute.For<ITypeReflector>();
            attributeUnderlyingType1.Equals(Arg.Any<ITypeReflector>()).Returns(true);

            var attributeTypes0 = new[]
            {
                Substitute.For<IAttributeReflector>()
            };
            attributeTypes0[0].Type.Returns(attributeUnderlyingType0);

            var attributeTypes1 = new[]
            {
                Substitute.For<IAttributeReflector>()
            };
            attributeTypes1[0].Type.Returns(attributeUnderlyingType1);

            var propertyTypes = new[]
            {
                Substitute.For<IPropertyReflector>(),
                Substitute.For<IPropertyReflector>()
            };
            propertyTypes[0].GetCustomAttributes().Returns(attributeTypes0);
            propertyTypes[1].GetCustomAttributes().Returns(attributeTypes1);

            var type = Substitute.For<ITypeReflector>();
            type.GetProperties().Returns(propertyTypes);

            var attributeType = Substitute.For<ITypeReflector>();

            var getter = new CsvColumnGetter();

            var specimen = getter.GetColumns(type, attributeType);

            specimen.Should().HaveCount(2);
            specimen[0].Attribute.Should().Be(attributeTypes0[0]);
            specimen[0].Property.Should().Be(propertyTypes[0]);
            specimen[1].Attribute.Should().Be(attributeTypes1[0]);
            specimen[1].Property.Should().Be(propertyTypes[1]);
        }
    }
}
