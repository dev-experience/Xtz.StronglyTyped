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
            Assert.That(value.Currency.Code.ToString().IsBogusGeneratedValue(), Is.False);
            Assert.That(value.Currency.Name.ToString().IsBogusGeneratedValue(), Is.False);
            Assert.That(value.Currency.Symbol?.ToString().IsBogusGeneratedValue(), Is.False);
        }

        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedProductFullName(ProductFullName value)
        {
            Assert.That(value.ProductAdjective.ToString().IsBogusGeneratedValue(), Is.False);
            Assert.That(value.ProductMaterial.ToString().IsBogusGeneratedValue(), Is.False);
            Assert.That(value.ProductShortName.ToString().IsBogusGeneratedValue(), Is.False);
        }
    }
}