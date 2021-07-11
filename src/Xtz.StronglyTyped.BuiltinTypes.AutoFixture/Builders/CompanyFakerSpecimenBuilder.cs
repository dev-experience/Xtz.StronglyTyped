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
            { typeof(CostCenterName), () => _builder.BuildCostCenterNameFaker() },
            { typeof(EnterpriseName), () => _builder.BuildEnterpriseNameFaker() },
            { typeof(JobArea), () => _builder.BuildJobAreaFaker() },
            { typeof(JobDescriptor), () => _builder.BuildJobDescriptorFaker() },
            { typeof(JobKey), () => _builder.BuildJobKeyFaker() },
            { typeof(JobTitle), () => _builder.BuildJobTitleFaker() },
            { typeof(JobType), () => _builder.BuildJobTypeFaker() },
            { typeof(SiteCode), () => _builder.BuildSiteCodeFaker() },
        };
    }
}