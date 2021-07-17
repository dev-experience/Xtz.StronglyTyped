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

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var expected = new StronglyTypedBool(value);

            //// Act

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var result = typeConverter.ConvertFrom(value) as StronglyTypedBool;

            //// Assert

            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result.Value, Is.EqualTo(expected.Value));
        }
    }
}