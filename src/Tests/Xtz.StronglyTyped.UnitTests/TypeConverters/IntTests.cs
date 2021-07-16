using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.UnitTests.TypeConverters
{
    public class IntTests
    {
        [Test]
        public void TypeConverter_ShouldParseStronglyTyped_GivenInt32()
        {
            //// Arrange

            var value = 47;
            var strongType = typeof(CountryId);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new CountryId(value);

            //// Act

            var result = typeConverter.ConvertFrom(value) as CountryId;

            //// Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(-1)]
        [Test]
        public void TypeConverter_ShouldThrow_GivenInvalidInt32(int value)
        {
            //// Arrange

            var strongType = typeof(CountryId);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            //// Act

            [ExcludeFromCodeCoverage]
            void Action() => typeConverter.ConvertFrom(value);

            //// Assert

            Assert.Throws<TypeConverterException>(Action);
        }
    }
}