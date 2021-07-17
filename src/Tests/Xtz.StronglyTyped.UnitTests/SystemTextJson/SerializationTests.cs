using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Xml;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.UnitTests.SystemTextJson
{
    public class SerializationTests
    {
        [Test]
        [TestCase("New York")]
        [TestCase("New \"The Big Apple\" York")]
        public void ShouldSerialize_GivenStronglyTypedStringClass(string value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedString(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase("New York")]
        [TestCase("New \"The Big Apple\" York")]
        public void ShouldSerialize_GivenStronglyTypedStringStruct(string value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedStringStruct(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase("Brazil")]
        [TestCase("Trinidad and Tobago")]
        [TestCase("")]
        public void ShouldSerialize_GivenStronglyTypedStringClassAllowEmpty(string value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedStringAllowEmpty(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase("Brazil")]
        [TestCase("Trinidad and Tobago")]
        [TestCase("")]
        public void ShouldSerialize_GivenStronglyTypedStringStructAllowEmpty(string value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedStringAllowEmptyStruct(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        // ReSharper disable once NUnit.IncorrectArgumentType
        [Test]
        [TestCase("61f6e72c-8db3-4a70-89b6-c3d07dbcce11")]
        public void ShouldSerialize_GivenStronglyTypedGuidClass(Guid value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedGuid(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        // ReSharper disable once NUnit.IncorrectArgumentType
        [Test]
        [TestCase("57c2a2d3-99cc-4468-998e-f3a3abe089ca")]
        public void ShouldSerialize_GivenStronglyTypedGuidStruct(Guid value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedGuidStruct(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        // ReSharper disable once NUnit.IncorrectArgumentType
        [Test]
        [TestCase("61f6e72c-8db3-4a70-89b6-c3d07dbcce11")]
        public void ShouldSerialize_GivenStronglyTypedGuidIdClass(Guid value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedGuidId(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6372)]
        [TestCase(int.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedIntClass(int value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedInt(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6372)]
        [TestCase(int.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedIntStruct(int value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedIntStruct(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(1)]
        [TestCase(546)]
        [TestCase(6474)]
        [TestCase(int.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedIntIdClass(int value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedIntId(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(false, "false")]
        [TestCase(true, "true")]
        public void ShouldSerialize_GivenStronglyTypedBoolClass(bool value, string expectedValue)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedBool(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        [TestCase(byte.MinValue)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(233)]
        [TestCase(byte.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedByteClass(byte value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedByte(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(char.MinValue)]
        [TestCase('I')]
        [TestCase('x')]
        [TestCase('\xF0')]
        [TestCase('Ž')]
        [TestCase('£')]
        [TestCase('¶')]
        [TestCase('Ð')]
        [TestCase('4')]
        [TestCase('ߡ')]
        [TestCase('ݓ')]
        [TestCase('Ѝ')]
        [TestCase(char.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedCharClass(char value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedChar(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        //[TestCase(-79228162514264337593543950335m)]
        [TestCase(-5745)]
        [TestCase(-968.2566)]
        //[TestCase(-968.2566m)]
        //[TestCase(-1m)]
        [TestCase(0)]
        //[TestCase(0m)]
        [TestCase(1)]
        //[TestCase(1m)]
        [TestCase(34634)]
        [TestCase(6534.353)]
        //[TestCase(79228162514264337593543950335m)]
        public void ShouldSerialize_GivenStronglyTypedByteClass(decimal value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedDecimal(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(double.MinValue)]
        [TestCase(double.Epsilon)]
        [TestCase(-5745)]
        [TestCase(-968.2566)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(34634)]
        [TestCase(6534.353)]
        [TestCase(double.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedDoubleClass(double value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedDouble(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(float.MinValue)]
        [TestCase(float.Epsilon)]
        [TestCase(-5745)]
        [TestCase(-968.2566F)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(34634)]
        [TestCase(6534.353F)]
        [TestCase(float.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedFloatClass(float value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedFloat(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(long.MinValue)]
        [TestCase(-5745)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(34634)]
        [TestCase(41343252352352)]
        [TestCase(long.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedLongClass(long value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedLong(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(sbyte.MinValue)]
        [TestCase(-55)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(34)]
        [TestCase(sbyte.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedSbyteClass(sbyte value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedSbyte(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(short.MinValue)]
        [TestCase(-24633)]
        [TestCase(-55)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(34)]
        [TestCase(7374)]
        [TestCase(short.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedShortClass(short value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedShort(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(uint.MinValue)]
        [TestCase(0U)]
        [TestCase(1U)]
        [TestCase(34U)]
        [TestCase(7374U)]
        [TestCase(45745745U)]
        [TestCase(uint.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedUintClass(uint value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedUint(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(ulong.MinValue)]
        [TestCase(0U)]
        [TestCase(1U)]
        [TestCase(34U)]
        [TestCase(7374U)]
        [TestCase(45745745U)]
        [TestCase(847847645745745U)]
        [TestCase(ulong.MaxValue)]
        public void ShouldSerialize_GivenStronglyTypedUlongClass(ulong value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedUlong(value);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        public void ShouldSerialize_GivenStronglyTypedUshortClass()
        {
            // Arrange

            var values = new ushort[] { ushort.MinValue, 0, 1, 34, 7374, ushort.MaxValue };

            var stronglyTyped = values.Select(x => (StronglyTypedUshort)x);
            var json = JsonSerializer.Serialize(values);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase("2020-08-14T14:35:57+0000")]
        [TestCase("2020-08-14T14:35:57-1000")]
        [TestCase("2020-08-14T14:35:57Z")]
        [TestCase("2320-07-21T23:00:35Z")]
        [TestCase("1539-05-06T09:20:45Z")]
        public void ShouldSerialize_GivenStronglyTypedDateTimeClass(DateTime value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedDateTime(value);
            var json = JsonSerializer.Serialize(value.ToUniversalTime());

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [TestCase(TimeSpan.TicksPerMillisecond)]
        [TestCase(TimeSpan.TicksPerSecond)]
        [TestCase(TimeSpan.TicksPerMinute)]
        [TestCase(TimeSpan.TicksPerHour)]
        [TestCase(TimeSpan.TicksPerDay)]
        public void ShouldSerialize_GivenStronglyTypedTimeSpanClass_FromTicks(long value)
        {
            // Arrange

            var timeSpan = TimeSpan.FromTicks(value);
            var stronglyTyped = new StronglyTypedTimeSpan(timeSpan);
            var expected = $"\"{XmlConvert.ToString(stronglyTyped)}\"";
            var expectedString = $"\"{stronglyTyped}\"";

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedString, result);
        }

        [Test]
        [TestCase("6")]
        [TestCase("6:12")]
        [TestCase("6:12:14")]
        [TestCase("6:12:14:45")]
        [TestCase("6.12:14:45")]
        [TestCase("6:12:14:45.3448")]
        public void ShouldSerialize_GivenStronglyTypedTimeSpanClass(TimeSpan value)
        {
            // Arrange

            var stronglyTyped = new StronglyTypedTimeSpan(value);
            var expected = $"\"{XmlConvert.ToString(value)}\"";
            var expectedString = $"\"{stronglyTyped}\"";

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedString, result);
        }

        [Test]
        [StrongAutoData]
        public void ShouldSerialize_GivenStronglyTypedEmailClass(Email stronglyTyped)
        {
            // Arrange

            var json = JsonSerializer.Serialize(stronglyTyped.Value.Address);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [StrongAutoData]
        public void ShouldSerialize_GivenStronglyTypedEmailsClass(IReadOnlyCollection<Email> stronglyTyped)
        {
            // Arrange

            var value = stronglyTyped.Select(x => x.Value.Address);
            var json = JsonSerializer.Serialize(value);

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(json, result);
        }

        [Test]
        [StrongAutoData]
        public void ShouldSerialize_GivenStronglyTypedIpV4AddressClass(IpV4Address stronglyTyped)
        {
            // Arrange

            var expected = $"\"{stronglyTyped.Value}\"";

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(expected, result);
        }

        [Test]
        [StrongAutoData]
        public void ShouldSerialize_GivenStronglyTypedIpV6AddressClass(IpV6Address stronglyTyped)
        {
            // Arrange

            var expected = $"\"{stronglyTyped.Value}\"";

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(expected, result);
        }

        [Test]
        [StrongAutoData]
        public void ShouldSerialize_GivenStronglyTypedMacAddressClass(MacAddress stronglyTyped)
        {
            // Arrange

            var expectedToString = $"\"{stronglyTyped.Value}\"";
            var expected = $"\"{stronglyTyped.ToString(MacAddress.Separator.Hyphen)}\"";

            // Act

            var result = JsonSerializer.Serialize(stronglyTyped);

            // Assert

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedToString, result.Replace("-", string.Empty));
        }
    }
}