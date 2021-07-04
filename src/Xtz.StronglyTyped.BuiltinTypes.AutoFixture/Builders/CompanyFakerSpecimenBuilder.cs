using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Company;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class CompanyFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly CompanyFakerBuilder _builder = new(true);

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(CompanyName), () => _builder.BuildCompanyNameFaker() },
            { typeof(CompanySuffix), () => _builder.BuildCompanySuffixFaker() },
            { typeof(CostCenterId), () => _builder.BuildCostCenterIdFaker() },
            { typeof(EnterpriseName), () => _builder.BuildEnterpriseNameFaker() },
        };
    }
}