using System;
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

            Assert.AreEqual(expected, result);
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

            Assert.AreEqual(value, result);
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

            Assert.AreEqual(value, result);
        }

        [TestCase(-123)]
        [TestCase(0)]
        [TestCase(3697)]
        [Test]
        public void Convert_ShouldConvertToInt_GivenStronglyTyped_WhenConvertingFromObject(int value)
        {
            //// Arrange

            object stronglyTyped = new EmployeeIntId(value);

            //// Act

            var result = Convert.ToInt32(stronglyTyped);

            //// Assert

            Assert.AreEqual(value, result);
        }
    }
}