using System.ComponentModel;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.UnitTests.TypeConverters
{
    public class StringTests
    {
        [Test]
        public void TypeConverter_ShouldParseStronglyTyped_GivenString()
        {
            //// Arrange

            var value = "Norway";
            var strongType = typeof(Country);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new Country(value);

            //// Act

            var result = typeConverter.ConvertFrom(value) as Country;

            //// Assert

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TypeConverter_ShouldReturnNull_GivenNull()
        {
            //// Arrange

            string value = null;
            var strongType = typeof(Country);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            //// Act

            var result = typeConverter.ConvertFrom(value);

            //// Assert

            Assert.IsNull(result);
        }

        [Test]
        public void TypeConverter_ShouldSerializeToString_GivenStronglyTyped()
        {
            //// Arrange

            var expected = "Norway";
            var strongType = typeof(Country);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var stronglyTyped = new Country(expected);

            //// Act

            var result = typeConverter.ConvertToString(stronglyTyped);

            //// Assert

            Assert.AreEqual(expected, result);
        }

        [TestCase("Sweden")]
        [TestCase("New Zealand")]
        [Test]
        public void TypeConverter_ShouldImplicitlyDeserializeTestCase_GivenString(Country value)
        {
            // Shouldn't fail

            //// Assert

            Assert.IsNotNull(value);
        }
    }
}