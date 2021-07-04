using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Commerce;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class CommerceFakerBuilder : BaseFakerBuilder
    {
        public CommerceFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// A random product color faker.
        /// </summary>
        public Faker<ProductColor> BuildColorFaker()
        {
            var result = GetFaker(() => new Faker<ProductColor>()
                .CustomInstantiator(f => new ProductColor(f.Commerce.Color())));
            return result;
        }

        /// <summary>
        /// A random commerce department faker.
        /// </summary>
        /// <remarks>Number of categories returned is between 1 and <see cref="max"/>.</remarks>
        public Faker<Department> BuildDepartmentFaker(int max = 3)
        {
            var result = GetFaker(() => new Faker<Department>()
                .CustomInstantiator(f => new Department(f.Commerce.Department(max))), max.ToString());
            return result;
        }

        /// <summary>
        /// A random EAN-13 barcode number faker.
        /// </summary>
        public Faker<Ean13> BuildEan13Faker()
        {
            var result = GetFaker(() => new Faker<Ean13>()
                .CustomInstantiator(f => new Ean13(f.Commerce.Ean13())));
            return result;
        }

        /// <summary>
        /// A random EAN-8 barcode number faker.
        /// </summary>
        public Faker<Ean8> BuildEan8Faker()
        {
            var result = GetFaker(() => new Faker<Ean8>()
                .CustomInstantiator(f => new Ean8(f.Commerce.Ean8())));
            return result;
        }

        /// <summary>
        /// A random product short name faker.
        /// </summary>
        public Faker<ProductShortName> BuildProductShortNameFaker()
        {
            var result = GetFaker(() => new Faker<ProductShortName>()
                .CustomInstantiator(f => new ProductShortName(f.Commerce.Product())));
            return result;
        }

        /// <summary>
        /// A random product adjective faker.
        /// </summary>
        public Faker<ProductAdjective> BuildProductAdjectiveFaker()
        {
            var result = GetFaker(() => new Faker<ProductAdjective>()
                .CustomInstantiator(f => new ProductAdjective(f.Commerce.ProductAdjective())));
            return result;
        }

        /// <summary>
        /// A random product category faker.
        /// </summary>
        public Faker<ProductCategory> BuildProductCategoryFaker()
        {
            var result = GetFaker(() => new Faker<ProductCategory>()
                .CustomInstantiator(f => new ProductCategory(f.Commerce.Categories(1)[0])));
            return result;
        }

        /// <summary>
        /// A random product material faker.
        /// </summary>
        public Faker<ProductMaterial> BuildProductMaterialFaker()
        {
            var result = GetFaker(() => new Faker<ProductMaterial>()
                .CustomInstantiator(f => new ProductMaterial(f.Commerce.ProductMaterial())));
            return result;
        }

        /// <summary>
        /// A random product full name faker.
        /// </summary>
        /// <remarks>{Adjective} {Material} {Product}</remarks>
        public Faker<ProductFullName> BuildProductFullNameFaker()
        {
            var result = GetFaker(() => new Faker<ProductFullName>()
                .CustomInstantiator(f =>
                {
                    var productAdjective = BuildProductAdjectiveFaker().Generate();
                    var productMaterial = BuildProductMaterialFaker().Generate();
                    var productShortName = BuildProductShortNameFaker().Generate();
                    return new ProductFullName(productAdjective, productMaterial, productShortName);
                }));
            return result;
        }

        /// <summary>
        /// A random commerce sub-department faker.
        /// </summary>
        public Faker<SubDepartment> BuildSubDepartmentFaker()
        {
            var result = GetFaker(() => new Faker<SubDepartment>()
                .CustomInstantiator(f => new SubDepartment(f.Commerce.Department(1))));
            return result;
        }
    }
}