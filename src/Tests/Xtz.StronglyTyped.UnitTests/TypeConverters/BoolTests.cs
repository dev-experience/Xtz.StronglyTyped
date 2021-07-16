using System.ComponentModel;
using NUnit.Framework;

namespace Xtz.StronglyTyped.UnitTests.TypeConverters
{
    public class BoolTests
    {
        [Test]
        public void TypeConverter_ShouldParseStronglyTyped_GivenBool()
        {
            //// Arrange

            var value = true;
            var strongType = typeof(StronglyTypedBool);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new StronglyTypedBool(value);

            //// Act

            var result = typeConverter.ConvertFrom(value) as StronglyTypedBool;

            //// Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected.Value, result.Value);
        }
    }
}