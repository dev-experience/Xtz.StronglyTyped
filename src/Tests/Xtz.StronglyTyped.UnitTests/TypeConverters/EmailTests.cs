using System.ComponentModel;
using System.Net.Mail;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.UnitTests.TypeConverters
{
    public class EmailTests
    {
        [TestCase("john.doe@example.com")]
        [TestCase("JOHN.DOE@example.com")]
        [TestCase("John.Doe@example.com")]
        [TestCase("john.doe+123@example.com")]
        [Test]
        public void TypeConverter_ShouldParseEmail_GivenString(string value)
        {
            //// Arrange

            var strongType = typeof(Email);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new Email(new MailAddress(value));

            //// Act

            var result = typeConverter.ConvertFrom(value);

            //// Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("john.doe@example.com", "JOHN.DOE@EXAMPLE.COM")]
        [TestCase("JOHN.DOE@example.com", "john.doe@EXAMPLE.COM")]
        [TestCase("John.Doe@example.com", "JOHN.DOE@EXAMPLE.COM")]
        [TestCase("john.doe+123@example.com", "JOHN.DOE+123@EXAMPLE.COM")]
        [Test]
        public void Email_ShouldBeEqual_GivenSameStringsDifferentCasing(string value, string expectedValue)
        {
            //// Arrange

            var strongType = typeof(Email);
            var typeConverter = TypeDescriptor.GetConverter(strongType);

            var expected = new Email(new MailAddress(expectedValue));

            //// Act

            var result = typeConverter.ConvertFrom(value) as Email;

            //// Assert

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}