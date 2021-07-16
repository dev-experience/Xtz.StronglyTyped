using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions;
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
            LastName lastName,
            NamePrefix namePrefix,
            NameSuffix nameSuffix)
        {
            var values = new object[]
            {
                displayName,
                firstName,
                lastName,
                namePrefix,
                nameSuffix,
            };

            Assert.That(values, Is.All.Matches<object>(x => !x.ToString()!.IsBogusGeneratedValue()));
        }

        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedFullName(FullName value)
        {
            Assert.IsFalse(value.FirstName.ToString().IsBogusGeneratedValue());
        }
    }
}