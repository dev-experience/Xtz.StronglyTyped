using NUnit.Framework;
using Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Phone;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class PhoneAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            PhoneNumber phoneNumber)
        {
            var values = new object[]
            {
                phoneNumber,
            };

            Assert.That(values, Is.All.Matches<object>(x => !x.ToString()!.IsBogusGeneratedValue()));
        }
    }
}