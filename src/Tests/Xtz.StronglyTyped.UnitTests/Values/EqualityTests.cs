using NUnit.Framework;

namespace Xtz.StronglyTyped.UnitTests.Values
{
    public class EqualityTests
    {
        [TestCase(27)]
        [Test]
        public void StronglyTypedValues_ShouldBeEqual_GivenSameInts(int value)
        {
            //// Arrange

            //// Act

            var result1 = new EmployeeIntId(value);
            var result2 = new EmployeeIntId(value);

            //// Assert

            Assert.AreEqual(result1, result2);
            Assert.AreEqual(result1.Value, result2.Value);
        }

        [TestCase(27, 45)]
        [Test]
        public void StronglyTypedValues_ShouldBeNotEqual_GivenDifferentInts(int value1, int value2)
        {
            //// Arrange

            //// Act

            var result1 = new EmployeeIntId(value1);
            var result2 = new EmployeeIntId(value2);

            //// Assert

            Assert.AreNotEqual(result1, result2);
            Assert.AreNotEqual(result1.Value, result2.Value);
        }

        [TestCase(22, 11)]
        [Test]
        public void ShouldBeEqual_GivenTheSameCalculatedInts(int value1, int value2)
        {
            //// Arrange

            //// Act

            var result1 = new EmployeeIntId(value1 + value2);
            var result2 = new EmployeeIntId(value1 + value2);

            //// Assert

            Assert.AreEqual(result1, result2);
            Assert.AreEqual(result1.Value, result2.Value);
        }

        [TestCase(27)]
        [Test]
        public void GetHastCode_ShouldBeEqual_GivenSameInts(int value)
        {
            //// Arrange

            //// Act

            var result1 = new EmployeeIntId(value);
            var result2 = new EmployeeIntId(value);

            //// Assert

            Assert.AreEqual(result1.GetHashCode(), result2.GetHashCode());
        }

        [TestCase(27, 45)]
        [Test]
        public void GetHastCode_ShouldBeNotEqual_GivenDifferentInts(int value1, int value2)
        {
            //// Arrange

            //// Act

            var result1 = new EmployeeIntId(value1);
            var result2 = new EmployeeIntId(value2);

            //// Assert

            Assert.AreNotEqual(result1.GetHashCode(), result2.GetHashCode());
        }
    }
}