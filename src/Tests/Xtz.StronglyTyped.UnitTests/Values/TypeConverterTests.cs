using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.UnitTests.Values
{
    public class TypeConverterTests
    {
        [Test]
        public void TypeConverter_ShouldParseStronglyTyped_GivenInt32()
        {
            //// Arrange

            var value = 47;
            var strongType = typeof(EmployeeIntId);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new EmployeeIntId(value);

            //// Act

            var result = typeConverter.ConvertFrom(value) as IStronglyTyped<int>;

            //// Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [TestCase(Int32.MinValue)]
        [TestCase(0)]
        [TestCase(-1)]
        [Test]
        public void TypeConverter_ShouldThrow_GivenInvalidInt32(int value)
        {
            //// Arrange

            var strongType = typeof(UserStructIntId);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            //// Act

            [ExcludeFromCodeCoverage]
            void Action() => typeConverter.ConvertFrom(value);

            //// Assert

            Assert.Throws<TypeConverterException>(Action);
        }
    }
}