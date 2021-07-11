using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Name;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class NameAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            DisplayName displayName,
            FirstName firstName,
            FullName fullName,
            LastName lastName,
            NamePrefix namePrefix,
            NameSuffix nameSuffix)
        {
            var values = new object[]
            {
                displayName,
                firstName,
                fullName,
                lastName,
                namePrefix,
                nameSuffix,
            };

            var nonBogusValues = values
                .Where(x => x.ToString()!.Length >= 36 && Guid.TryParse(x.ToString()![^36..], out _));

            Assert.IsEmpty(nonBogusValues);
        }
    }
}