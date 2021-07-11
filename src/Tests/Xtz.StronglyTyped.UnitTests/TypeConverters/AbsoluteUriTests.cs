using System;
using System.ComponentModel;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Internet;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.UnitTests.TypeConverters
{
    public class AbsoluteUriTests
    {
        [Test]
        public void TypeConverter_ShouldParseGenericStronglyTyped_GivenString()
        {
            //// Arrange

            var value = "https://example.com";
            var strongType = typeof(AbsoluteUri);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new AbsoluteUri(value);

            //// Act

            var result = typeConverter.ConvertFrom(value) as AbsoluteUri;

            //// Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [Test]
        public void TypeConverter_ShouldParseStronglyTyped_GivenInnerType()
        {
            //// Arrange

            var value = new Uri("https://example.com");
            var strongType = typeof(AbsoluteUri);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new AbsoluteUri(value);

            //// Act

            var result = typeConverter.ConvertFrom(value) as AbsoluteUri;

            //// Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [Test]
        public void TypeConverter_ShouldFailToParseStronglyTyped_GivenInvalidValue()
        {
            //// Arrange

            var value = "/api";
            var strongType = typeof(AbsoluteUri);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            //// Act

            TestDelegate action = () => typeConverter.ConvertFrom(value);

            //// Assert

            Assert.Throws<TypeConverterException>(action);
        }
    }
}