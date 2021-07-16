using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Address;

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

        [TestCase(Int32.MinValue)]
        [TestCase(-123)]
        [TestCase(0)]
        [TestCase(73)]
        [TestCase(Int32.MaxValue)]
        [Test]
        public void ShouldReturnInnerValue_WhenToString(int value)
        {
            //// Arrange

            var expected = value.ToString();

            //// Act

            var stronglyTyped = new EmployeeIntId(value);

            //// Assert

            Assert.AreEqual(expected, stronglyTyped.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [Test]
        public void ShouldThrow_GivenNullOrEmpty(string country)
        {
            //// Arrange


            //// Act

            [ExcludeFromCodeCoverage]
            void Action() => new Country(country);

            //// Assert

            Assert.Throws<InvalidValueException>(Action);
        }

        [Test]
        public void ShouldThrow_GivenNull_ForAllowEmpty()
        {
            //// Arrange

            string value = null;

            //// Act

            [ExcludeFromCodeCoverage]
            void Action() => new StronglyTypedStringAllowEmpty(value);

            //// Assert

            Assert.Throws<InvalidValueException>(Action);
        }

        [Test]
        public void ShouldCreate_GivenEmpty_ForAllowEmpty()
        {
            //// Arrange

            var value = string.Empty;

            //// Act

            var result = new StronglyTypedStringAllowEmpty(value);

            //// Assert

            Assert.NotNull(result);
            Assert.IsEmpty(result.Value);
            Assert.IsEmpty(value.ToString());
        }
    }
}