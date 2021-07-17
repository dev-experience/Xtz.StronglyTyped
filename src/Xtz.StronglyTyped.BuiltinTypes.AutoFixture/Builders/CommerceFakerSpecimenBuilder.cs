using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Commerce;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class CommerceFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly CommerceFakerBuilder _builder = new();

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(ProductColor), () => _builder.BuildColorFaker() },
            { typeof(Department), () => _builder.BuildDepartmentFaker() },
            { typeof(Ean13), () => _builder.BuildEan13Faker() },
            { typeof(Ean8), () => _builder.BuildEan8Faker() },
            { typeof(ProductShortName), () => _builder.BuildProductShortNameFaker() },
            { typeof(ProductAdjective), () => _builder.BuildProductAdjectiveFaker() },
            { typeof(ProductCategory), () => _builder.BuildProductCategoryFaker() },
            { typeof(ProductMaterial), () => _builder.BuildProductMaterialFaker() },
            { typeof(ProductFullName), () => _builder.BuildProductFullNameFaker() },
            { typeof(SubDepartment), () => _builder.BuildSubDepartmentFaker() },
        };
    }
}