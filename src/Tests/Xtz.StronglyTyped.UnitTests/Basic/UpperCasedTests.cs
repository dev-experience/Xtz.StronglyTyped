using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Strings;

namespace Xtz.StronglyTyped.UnitTests.Basic
{
    public class UpperCasedTests
    {
        [TestCase("strong type")]
        [TestCase("Strong Type")]
        [TestCase("STRONG TYPE")]
        [Test]
        public void StronglyTyped_ShouldBeUpperCased_GivenCasedString(string value)
        {
            //// Arrange

            var expected = value.ToUpperInvariant();

            //// Act

            var result = new UpperCased(value);

            //// Assert

            Assert.AreEqual(expected, result.Value);
        }
    }
}