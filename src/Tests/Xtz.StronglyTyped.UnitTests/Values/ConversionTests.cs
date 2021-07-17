using NUnit.Framework;

namespace Xtz.StronglyTyped.UnitTests.Values
{
    public class ConversionTests
    {
        [TestCase(-123)]
        [TestCase(0)]
        [TestCase(3697)]
        [Test]
        public void ShouldCreateEmployeeIntId_GivenInt_WhenExplicitlyConverted(int value)
        {
            //// Arrange

            var expected = new EmployeeIntId(value);

            //// Act

            var result = (EmployeeIntId)value;

            //// Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(-123)]
        [TestCase(0)]
        [TestCase(3697)]
        [Test]
        public void ShouldConvertToInt_GivenEmployeeIntId_WhenExplicitlyConverted(int value)
        {
            //// Arrange

            var stronglyTyped = new EmployeeIntId(value);

            //// Act

            var result = (int)stronglyTyped;

            //// Assert

            Assert.That(result, Is.EqualTo(value));
        }

        [TestCase(-123)]
        [TestCase(0)]
        [TestCase(3697)]
        [Test]
        public void ShouldConvertToInt_GivenEmployeeIntId_WhenImplicitlyConverted(int value)
        {
            //// Arrange

            var stronglyTyped = new EmployeeIntId(value);

            //// Act

            int result = stronglyTyped;

            //// Assert

            Assert.That(result, Is.EqualTo(value));
        }
    }
}