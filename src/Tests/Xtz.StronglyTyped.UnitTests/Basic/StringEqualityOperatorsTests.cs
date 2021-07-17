using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Address;

namespace Xtz.StronglyTyped.UnitTests.Basic
{
    public class StringEqualityOperatorsTests
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
            Assert.That(value == value, Is.True);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Test]
        public void EqualsOperator_ShouldBeTrue_ForSameValuesStronglyTypedAndObject()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = value as object;

            //// Assert

            Assert.That(value == objValue, Is.True);
            Assert.That(objValue == value, Is.True);
        }

        [Test]
        public void EqualsOperator_ShouldBeTrue_ForStronglyTypedValues_GivenSameStrings()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = new Country("Norway");

            //// Assert

            Assert.That(value1 == value2, Is.True);
            Assert.That(value2 == value1, Is.True);
        }

        [Test]
        public void EqualsOperator_ShouldBeTrue_ForStronglyTypedAndObject_GivenSameStrings()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = new Country("Norway") as object;

            //// Assert

            Assert.That(value == objValue, Is.True);
            Assert.That(objValue == value, Is.True);
        }

        [Test]
        public void EqualsOperator_ShouldBeFalse_ForStronglyTypedValues_GivenDifferentStrings()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = new Country("Denmark");

            //// Assert

            Assert.That(value1 == value2, Is.False);
            Assert.That(value2 == value1, Is.False);
        }

        [Test]
        public void EqualsOperator_ShouldBeFalse_ForStronglyTypedAndObject_GivenDifferentStrings()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = new Country("Denmark") as object;

            //// Assert

            Assert.That(value == objValue, Is.False);
            Assert.That(objValue == value, Is.False);
        }

        [Test]
        public void NotEqualsOperator_ShouldBeFalse_ForStronglyTypedValues_GivenSameStrings()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = new Country("Norway");

            //// Assert

            Assert.That(value1 != value2, Is.False);
            Assert.That(value2 != value1, Is.False);
        }

        [Test]
        public void NotEqualsOperator_ShouldBeFalse_ForStronglyTypedAndObject_GivenSameStrings()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = new Country("Norway") as object;

            //// Assert

            Assert.That(value != objValue, Is.False);
            Assert.That(objValue != value, Is.False);
        }

        [Test]
        public void NotEqualsOperator_ShouldBeTrue_ForStronglyTypedValues_GivenDifferentStrings()
        {
            //// Arrange

            //// Act

            var value1 = new Country("Norway");
            var value2 = new Country("Denmark");

            //// Assert

            Assert.That(value1 != value2, Is.True);
            Assert.That(value2 != value1, Is.True);
        }

        [Test]
        public void NotEqualsOperator_ShouldBeTrue_ForStronglyTypedAndObject_GivenDifferentStrings()
        {
            //// Arrange

            //// Act

            var value = new Country("Norway");
            var objValue = new Country("Denmark") as object;

            //// Assert

            Assert.That(value != objValue, Is.True);
            Assert.That(objValue != value, Is.True);
        }
    }
}