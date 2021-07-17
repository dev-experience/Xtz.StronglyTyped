using NUnit.Framework;
using Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions;
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
            ProductShortName productShortName,
            ProductAdjective productAdjective,
            ProductCategory productCategory,
            ProductColor productColor,
            ProductMaterial productMaterial,
            SubDepartment subDepartment)
        {
            var values = new object[]
            {
                department,
                ean13,
                ean8,
                productShortName,
                productAdjective,
                productCategory,
                productColor,
                productMaterial,
                subDepartment,
            };

            Assert.That(values, Is.All.Matches<object>(x => !x.ToString()!.IsBogusGeneratedValue()));
        }

        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedPrice(Price value)
        {
            Assert.IsFalse(value.Currency.Code.ToString().IsBogusGeneratedValue());
            Assert.IsFalse(value.Currency.Name.ToString().IsBogusGeneratedValue());
            Assert.IsFalse(value.Currency.Symbol?.ToString().IsBogusGeneratedValue());
        }

        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedProductFullName(ProductFullName value)
        {
            Assert.IsFalse(value.ProductAdjective.ToString().IsBogusGeneratedValue());
            Assert.IsFalse(value.ProductMaterial.ToString().IsBogusGeneratedValue());
            Assert.IsFalse(value.ProductShortName.ToString().IsBogusGeneratedValue());
        }
    }
}