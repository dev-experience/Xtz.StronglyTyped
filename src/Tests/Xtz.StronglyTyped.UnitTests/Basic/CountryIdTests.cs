using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.UnitTests.Basic
{
    public class CountryIdTests
    {
        [TestCase(Int32.MaxValue)]
        [TestCase(1)]
        [TestCase(23)]
        [Test]
        public void CountryId_ShouldSucceed_GivenValidValue(int value)
        {
            //// Arrange

            //// Act

            var result = new CountryId(value);

            //// Assert

            Assert.That(result.Value, Is.EqualTo(value));
        }

        [TestCase(Int32.MinValue)]
        [TestCase(0)]
        [TestCase(-1)]
        [Test]
        public void CountryId_ShouldThrow_GivenInvalidValue(int value)
        {
            //// Arrange

            //// Act

            [ExcludeFromCodeCoverage]
            // ReSharper disable once ObjectCreationAsStatement
            void Action() => new CountryId(value);

            //// Assert

            Assert.Throws<InvalidValueException>(Action);
        }

        // Long.MaxValue
        [TestCase(0x7fffffffffffffffL)]
        [TestCase("Text value")]
        [Test]
        public void CountryIdImplicitOperator_ShouldThrow_GivenDifferentType(object value)
        {
            //// Arrange

            //// Act

            [ExcludeFromCodeCoverage]
            // ReSharper disable once UnusedLocalFunctionReturnValue
            CountryId Action() => (CountryId)value;

            //// Assert

            Assert.Throws<InvalidCastException>(() => Action());
        }
    }
}