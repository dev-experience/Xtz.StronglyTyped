using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Commerce;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class CommerceAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            Department department,
            Ean13 ean13,
            Ean8 ean8,
            Price price,
            ProductShortName productShortName,
            ProductAdjective productAdjective,
            ProductCategory productCategory,
            ProductColor productColor,
            ProductMaterial productMaterial,
            ProductFullName productFullName,
            SubDepartment subDepartment)
        {
            var values = new object[]
            {
                department,
                ean13,
                ean8,
                price,
                productShortName,
                productAdjective,
                productCategory,
                productColor,
                productMaterial,
                productFullName,
                subDepartment,
            };

            var nonBogusValues = values
                .Where(x => x.ToString()!.Length >= 36 && Guid.TryParse(x.ToString()![^36..], out _));

            Assert.IsEmpty(nonBogusValues);
        }
    }
}