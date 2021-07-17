using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Internet;
using Xtz.StronglyTyped.UnitTests;

namespace Xtz.StronglyTyped.NewtonsoftJson.UnitTests
{
    public class DeserializationTests
    {
        private static readonly JsonSerializerSettings JSON_SERIALIZER_SETTINGS;

        static DeserializationTests()
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new StronglyTypedNewtonsoftConverter());
            JSON_SERIALIZER_SETTINGS = jsonSerializerSettings;
        }

        [Test]
        [TestCase("New York")]
        [TestCase("New \"The Big Apple\" York")]
        public void ShouldDeserialize_ToStronglyTypedStringClass(string value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedString>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedString>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase("New York")]
        [TestCase("New \"The Big Apple\" York")]
        public void ShouldDeserialize_ToStronglyTypedStringStruct(string value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedStringStruct>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedStringStruct>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase("Brazil")]
        [TestCase("Trinidad and Tobago")]
        [TestCase("")]
        public void ShouldDeserialize_ToStronglyTypedStringClassAllowEmpty(string value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedStringAllowEmpty>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedStringAllowEmpty>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase("Brazil")]
        [TestCase("Trinidad and Tobago")]
        [TestCase("")]
        public void ShouldDeserialize_ToStronglyTypedStringStructAllowEmpty(string value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedStringAllowEmptyStruct>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedStringAllowEmptyStruct>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        // ReSharper disable once NUnit.IncorrectArgumentType
        [Test]
        [TestCase("61f6e72c-8db3-4a70-89b6-c3d07dbcce11")]
        public void ShouldDeserialize_ToStronglyTypedGuidClass(Guid value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedGuid>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedGuid>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        // ReSharper disable once NUnit.IncorrectArgumentType
        [Test]
        [TestCase("57c2a2d3-99cc-4468-998e-f3a3abe089ca")]
        public void ShouldDeserialize_ToStronglyTypedGuidStruct(Guid value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedGuidStruct>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedGuidStruct>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        // ReSharper disable once NUnit.IncorrectArgumentType
        [Test]
        [TestCase("61f6e72c-8db3-4a70-89b6-c3d07dbcce11")]
        public void ShouldDeserialize_ToStronglyTypedGuidIdClass(Guid value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedGuidId>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedGuidId>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6372)]
        [TestCase(int.MaxValue)]
        public void ShouldDeserialize_ToStronglyTypedIntClass(int value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedInt>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedInt>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6372)]
        [TestCase(int.MaxValue)]
        public void ShouldDeserialize_ToStronglyTypedIntStruct(int value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedIntStruct>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedIntStruct>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase(1)]
        [TestCase(546)]
        [TestCase(6474)]
        [TestCase(int.MaxValue)]
        public void ShouldDeserialize_ToStronglyTypedIntIdClass(int value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedIntId>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedIntId>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        public void ShouldDeserialize_ToStronglyTypedBoolClass()
        {
            // Arrange

            var values = new[] { false, true };

            var stronglyTyped = values
                .Select(x => new SerializationDto<StronglyTypedBool>(x))
                .ToArray();
            var json = "[{\"testValue\": false }, {\"testValue\": true }]";

            // Act

            var result = JsonConvert.DeserializeObject<IReadOnlyCollection<SerializationDto<StronglyTypedBool>>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(result, Has.Exactly(stronglyTyped.Length).Items);
            Assert.That(result, Is.All.Matches<SerializationDto<StronglyTypedBool>>(x => stronglyTyped.Any(s => s.TestValue == x.TestValue)));
        }

        [Test]
        [TestCase(byte.MinValue)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(233)]
        [TestCase(byte.MaxValue)]
        public void ShouldDeserialize_ToStronglyTypedByteClass(byte value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedByte>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedByte>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
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
        public void ShouldDeserialize_ToStronglyTypedCharClass(char value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedChar>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedChar>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase(-5745)]
        [TestCase(-968.2566)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(34634)]
        [TestCase(6534.353)]
        public void ShouldDeserialize_ToStronglyTypedDecimalClass(decimal value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedDecimal>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedDecimal>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        public void ShouldDeserialize_ToStronglyTypedDecimalClass()
        {
            // Arrange

            var values = new[] { decimal.MinValue, -968.2566m, decimal.MinusOne, decimal.Zero, decimal.One, decimal.MaxValue };

            var stronglyTyped = values
                .Select(x => new SerializationDto<StronglyTypedDecimal>(x))
                .ToArray();
            var json = BuildNonStringArray(stronglyTyped.Select(x => x.TestValue.Value).Cast<object>());

            // Act

            var result = JsonConvert.DeserializeObject<IReadOnlyCollection<SerializationDto<StronglyTypedDecimal>>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(result, Has.Exactly(stronglyTyped.Length).Items);
            Assert.That(result, Is.All.Matches<SerializationDto<StronglyTypedDecimal>>(x => stronglyTyped.Any(s => s.TestValue == x.TestValue)));
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
        public void ShouldDeserialize_ToStronglyTypedDoubleClass(double value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedDouble>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedDouble>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
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
        public void ShouldDeserialize_ToStronglyTypedFloatClass(float value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedFloat>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedFloat>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase(long.MinValue)]
        [TestCase(-5745)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(34634)]
        [TestCase(41343252352352)]
        [TestCase(long.MaxValue)]
        public void ShouldDeserialize_ToStronglyTypedLongClass(long value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedLong>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedLong>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase(sbyte.MinValue)]
        [TestCase(-55)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(34)]
        [TestCase(sbyte.MaxValue)]
        public void ShouldDeserialize_ToStronglyTypedSbyteClass(sbyte value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedSbyte>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedSbyte>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
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
        public void ShouldDeserialize_ToStronglyTypedShortClass(short value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedShort>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedShort>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase(uint.MinValue)]
        [TestCase(0U)]
        [TestCase(1U)]
        [TestCase(34U)]
        [TestCase(7374U)]
        [TestCase(45745745U)]
        [TestCase(uint.MaxValue)]
        public void ShouldDeserialize_ToStronglyTypedUintClass(uint value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedUint>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedUint>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
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
        public void ShouldDeserialize_ToStronglyTypedUlongClass(ulong value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedUlong>(value);
            var json = BuildNonString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedUlong>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        public void ShouldDeserialize_ToStronglyTypedUshortClass()
        {
            var values = new[] { ushort.MinValue, (ushort)0, (ushort)1, (ushort)34, (ushort)7374, ushort.MaxValue };
            // Arrange

            var stronglyTyped = values
                .Select(x => new SerializationDto<StronglyTypedUshort>(x))
                .ToArray();
            var json = BuildNonStringArray(stronglyTyped.Select(x => x.TestValue.Value).Cast<object>());

            // Act

            var result = JsonConvert.DeserializeObject<IReadOnlyCollection<SerializationDto<StronglyTypedUshort>>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(result, Has.Exactly(stronglyTyped.Length).Items);
            Assert.That(result, Is.All.Matches<SerializationDto<StronglyTypedUshort>>(x => stronglyTyped.Any(s => s.TestValue == x.TestValue)));
        }

        [Test]
        [TestCase("2020-08-14T14:35:57+0000")]
        [TestCase("2020-08-14T14:35:57-1000")]
        [TestCase("2020-08-14T14:35:57Z")]
        [TestCase("2320-07-21T23:00:35Z")]
        [TestCase("1539-05-06T09:20:45Z")]
        public void ShouldDeserialize_ToStronglyTypedDateTimeClass(DateTime value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedDateTime>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedDateTime>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase(TimeSpan.TicksPerMillisecond)]
        [TestCase(TimeSpan.TicksPerSecond)]
        [TestCase(TimeSpan.TicksPerMinute)]
        [TestCase(TimeSpan.TicksPerHour)]
        [TestCase(TimeSpan.TicksPerDay)]
        public void ShouldDeserialize_ToStronglyTypedTimeSpanClass_FromTicks(long value)
        {
            var timeSpan = TimeSpan.FromTicks(value);
            var expected = XmlConvert.ToString(timeSpan);

            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedTimeSpan>(timeSpan);
            var json = BuildString(expected);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedTimeSpan>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [TestCase("6")]
        [TestCase("6:12")]
        [TestCase("6:12:14")]
        [TestCase("6:12:14:45")]
        [TestCase("6.12:14:45")]
        [TestCase("6:12:14:45.3448")]
        public void ShouldDeserialize_ToStronglyTypedTimeSpanClass(TimeSpan timeSpan)
        {
            var expected = XmlConvert.ToString(timeSpan);

            // Arrange

            var stronglyTyped = new SerializationDto<StronglyTypedTimeSpan>(timeSpan);
            var json = BuildString(expected);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<StronglyTypedTimeSpan>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [StrongAutoData]
        public void ShouldDeserialize_ToStronglyTypedEmailClass(Email value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<Email>(value);
            var json = BuildString(stronglyTyped.TestValue.Value.Address);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<Email>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [StrongAutoData]
        public void ShouldDeserialize_ToStronglyTypedEmailsClass(IReadOnlyCollection<Email> values)
        {
            // Arrange

            var stronglyTyped = values
                .Select(x => new SerializationDto<Email>(x))
                .ToArray();
            var json = BuildStringArray(stronglyTyped.Select(x => x.TestValue.Value.Address));

            // Act

            var result = JsonConvert.DeserializeObject<IReadOnlyCollection<SerializationDto<Email>>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(result, Has.Exactly(stronglyTyped.Length).Items);
            Assert.That(result, Is.All.Matches<SerializationDto<Email>>(x => stronglyTyped.Any(s => s.TestValue == x.TestValue)));
        }

        [Test]
        [StrongAutoData]
        public void ShouldDeserialize_ToStronglyTypedIpV4AddressClass(IpV4Address value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<IpV4Address>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<IpV4Address>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [StrongAutoData]
        public void ShouldDeserialize_ToStronglyTypedIpV6AddressClass(IpV6Address value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<IpV6Address>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<IpV6Address>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        [Test]
        [StrongAutoData]
        public void ShouldDeserialize_ToStronglyTypedMacAddressClass(MacAddress value)
        {
            // Arrange

            var stronglyTyped = new SerializationDto<MacAddress>(value);
            var json = BuildString(value);

            // Act

            var result = JsonConvert.DeserializeObject<SerializationDto<MacAddress>>(json, JSON_SERIALIZER_SETTINGS);

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(stronglyTyped.TestValue, result.TestValue);
        }

        private static string BuildStringArray(IEnumerable<object> values)
        {
            var stringBuilder = new StringBuilder("[ \n");

            foreach (var value in values)
            {
                stringBuilder.AppendFormat("  {0},\n", BuildString(value));
            }

            stringBuilder.Append(']');
            var result = stringBuilder.ToString();
            return result;
        }

        private static string BuildNonStringArray(IEnumerable<object> values)
        {
            var stringBuilder = new StringBuilder("[ \n");

            foreach (var value in values)
            {
                stringBuilder.AppendFormat("  {0},\n", BuildNonString(value));
            }

            stringBuilder.Append(']');
            var result = stringBuilder.ToString();
            return result;
        }

        private static string BuildString(object value)
        {
            return $"{{ \"testValue\": \"{value?.ToString()?.Replace("\"", "\\\"")}\" }}";
        }

        private static string BuildNonString(object value)
        {
            return $"{{ \"testValue\": {value} }}";
        }
    }
}