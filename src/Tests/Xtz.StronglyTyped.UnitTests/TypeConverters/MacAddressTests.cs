using System.ComponentModel;
using System.Net.NetworkInformation;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.UnitTests.TypeConverters
{
    public class MacAddressTests
    {
        [TestCase("00:11:22:33:44:55")]
        [TestCase("00-11-22-33-44-55")]
        [TestCase("A0:A1:A2:A3:A4:A5")]
        [TestCase("A0-A1-A2-A3-A4-A5")]
        [TestCase("554433221100")]
        [TestCase("ffeeddbbccaa")]
        [TestCase("FFEEDDBBCCAA")]
        [Test]
        public void TypeConverter_ShouldParseStronglyTyped_GivenString(string value)
        {
            //// Arrange

            var strongType = typeof(MacAddress);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expectedInnerValue = ValueToString(value);
            var expected = new MacAddress(PhysicalAddress.Parse(expectedInnerValue));

            //// Act

            var result = typeConverter.ConvertFrom(value);

            //// Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("00:11:22:33:44:55")]
        [TestCase("00-11-22-33-44-55")]
        [TestCase("A0:A1:A2:A3:A4:A5")]
        [TestCase("A0-A1-A2-A3-A4-A5")]
        [TestCase("554433221100")]
        [TestCase("ffeeddbbccaa")]
        [TestCase("FFEEDDBBCCAA")]
        [Test]
        public void TypeConverter_ShouldParseWrappingStronglyTyped_GivenInnerType(string value)
        {
            //// Arrange

            var innerValue = PhysicalAddress.Parse(ValueToString(value));
            var strongType = typeof(MacAddress);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new MacAddress(innerValue);

            //// Act

            var result = typeConverter.ConvertFrom(innerValue);

            //// Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("00:11:22:33:44:55")]
        [TestCase("00-11-22-33-44-55")]
        [TestCase("A0:A1:A2:A3:A4:A5")]
        [TestCase("A0-A1-A2-A3-A4-A5")]
        [TestCase("554433221100")]
        [TestCase("ffeeddbbccaa")]
        [TestCase("FFEEDDBBCCAA")]
        [Test]
        public void TypeConverter_ShouldConvertToString_GivenStronglyTyped(string value)
        {
            //// Arrange

            var stronglyTypedValue = new MacAddress(PhysicalAddress.Parse(ValueToString(value)));
            var strongType = typeof(MacAddress);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = ValueToString(value);

            //// Act

            var result = typeConverter.ConvertToString(stronglyTypedValue);

            //// Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("00:11:22:33:44:55")]
        [TestCase("00-11-22-33-44-55")]
        [TestCase("A0:A1:A2:A3:A4:A5")]
        [TestCase("A0-A1-A2-A3-A4-A5")]
        [TestCase("554433221100")]
        [TestCase("ffeeddbbccaa")]
        [TestCase("FFEEDDBBCCAA")]
        [Test]
        public void TypeConverter_ShouldConvertToString_WhenImplicitlyCastedToString(string value)
        {
            //// Arrange

            var stronglyTypedValue = new MacAddress(PhysicalAddress.Parse(ValueToString(value)));
            var strongType = typeof(MacAddress);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = ValueToString(value);

            //// Act

            var result = typeConverter.ConvertToString(stronglyTypedValue);

            //// Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        private static string ValueToString(string value)
        {
            var normalized = value
                .Replace(":", string.Empty)
                .Replace("-", string.Empty)
                .ToUpperInvariant();
            var result = string.Format(
                "{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                "-",
                normalized[0..2],
                normalized[2..4],
                normalized[4..6],
                normalized[6..8],
                normalized[8..10],
                normalized[10..12]);
            return result;
        }
    }
}