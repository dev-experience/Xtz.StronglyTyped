using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.UnitTests.Values
{
    public class EqualityOperatorsTests
    {
        [Test]
        public void EqualsOperator_ShouldBeTrue_ForSameStronglyTypedValues()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");

            //// Assert

#pragma warning disable CS1718 // Comparison made to same variable
            // ReSharper disable once EqualExpressionComparison
            Assert.IsTrue(value == value);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Test]
        public void EqualsOperator_ShouldBeTrue_ForSameValuesStronglyTypedAndObject()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            // ReSharper disable once TryCastAlwaysSucceeds
            var objValue = value as object;

            //// Assert

            Assert.IsTrue(value == objValue);
            Assert.IsTrue(objValue == value);
        }

        [Test]
        public void EqualsOperator_ShouldBeFalse_ForStronglyTypedValue_AndSameString()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = "Norway";

            //// Assert

            Assert.IsFalse(value1 == value2);
            Assert.IsFalse(value2 == value1);
        }

        [Test]
        public void EqualsOperator_ShouldBeTrue_ForStronglyTypedValues_GivenSameStrings()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = new Country("Norway");

            //// Assert

            Assert.IsTrue(value1 == value2);
            Assert.IsTrue(value2 == value1);
        }

        [Test]
        public void EqualsOperator_ShouldBeTrue_ForStronglyTypedAndObject_GivenSameStrings()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = new Country("Norway") as object;

            //// Assert

            Assert.IsTrue(value == objValue);
            Assert.IsTrue(objValue == value);
        }

        [Test]
        public void EqualsOperator_ShouldBeFalse_ForStronglyTypedValues_GivenDifferentStrings()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = new Country("Denmark");

            //// Assert

            Assert.IsFalse(value1 == value2);
            Assert.IsFalse(value2 == value1);
        }

        [Test]
        public void EqualsOperator_ShouldBeFalse_ForStronglyTypedAndObject_GivenDifferentStrings()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = new Country("Denmark") as object;

            //// Assert

            Assert.IsFalse(value == objValue);
            Assert.IsFalse(objValue == value);
        }

        [Test]
        public void NotEqualsOperator_ShouldBeFalse_ForStronglyTypedValues_GivenSameStrings()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = new Country("Norway");

            //// Assert

            Assert.IsFalse(value1 != value2);
            Assert.IsFalse(value2 != value1);
        }

        [Test]
        public void NotEqualsOperator_ShouldBeFalse_ForStronglyTypedAndObject_GivenSameStrings()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = new Country("Norway") as object;

            //// Assert

            Assert.IsFalse(value != objValue);
            Assert.IsFalse(objValue != value);
        }

        [Test]
        public void NotEqualsOperator_ShouldBeTrue_ForStronglyTypedValues_GivenDifferentStrings()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = new Country("Denmark");

            //// Assert

            Assert.IsTrue(value1 != value2);
            Assert.IsTrue(value2 != value1);
        }

        [Test]
        public void NotEqualsOperator_ShouldBeTrue_ForStronglyTypedAndObject_GivenDifferentStrings()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = new Country("Denmark") as object;

            //// Assert

            Assert.IsTrue(value != objValue);
            Assert.IsTrue(objValue != value);
        }
    }
}