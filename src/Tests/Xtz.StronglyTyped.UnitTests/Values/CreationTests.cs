using System;
using NUnit.Framework;

namespace Xtz.StronglyTyped.UnitTests.Values
{
    public class CreationTests
    {
        [TestCase(Int32.MinValue)]
        [TestCase(-123)]
        [TestCase(0)]
        [TestCase(73)]
        [TestCase(Int32.MaxValue)]
        [Test]
        public void ShouldCreate_GivenInt(int value)
        {
            //// Arrange

            //// Act

            var result = new EmployeeIntId(value);

            //// Assert

            Assert.AreEqual(value, result.Value);
        }

        [TestCase(0)]
        [TestCase(73)]
        [Test]
        public void ShouldReturnInnerValue_WhenToString(int value)
        {
            //// Arrange

            var stronglyTyped = new EmployeeIntId(value);
            var expected = value.ToString();

            //// Act

            var result = stronglyTyped.ToString();

            //// Assert

            Assert.AreEqual(expected, result);
        }
    }
}