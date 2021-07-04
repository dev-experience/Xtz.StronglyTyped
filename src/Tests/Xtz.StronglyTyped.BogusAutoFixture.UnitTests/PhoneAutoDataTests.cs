using System;
using System.Linq;
using NUnit.Framework;
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

            var nonBogusValues = values
                .Where(x => x.ToString()!.Length >= 36 && Guid.TryParse(x.ToString()![^36..], out _));

            Assert.IsEmpty(nonBogusValues);
        }
    }
}