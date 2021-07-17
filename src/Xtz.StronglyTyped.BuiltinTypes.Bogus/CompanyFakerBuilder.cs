using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Company;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class CompanyFakerBuilder : BaseFakerBuilder
    {
        private readonly CommerceFakerBuilder _commerceFakerBuilder;

        public CompanyFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
            _commerceFakerBuilder = new CommerceFakerBuilder(useFakerCache);
        }

        /// <summary>
        /// A random company name faker.
        /// </summary>
        /// <param name="formatIndex">0: name + suffix, 1: name-name, 2: name, name and name."</param>
        public Faker<CompanyName> BuildCompanyNameFaker(int? formatIndex = null)
        {
            var result = GetFaker(() => new Faker<CompanyName>()
                .CustomInstantiator(f => new CompanyName(f.Company.CompanyName(formatIndex))), formatIndex.ToString());
            return result;
        }

        /// <summary>
        /// A random company suffix faker.
        /// </summary>
        /// <remarks>Example: "Inc", "LLC", etc.</remarks>
        public Faker<CompanySuffix> BuildCompanySuffixFaker()
        {
            var result = GetFaker(() => new Faker<CompanySuffix>()
                .CustomInstantiator(f => new CompanySuffix(f.Company.CompanySuffix())));
            return result;
        }

        /// <summary>
        /// A random cost center ID faker (from 1 to 1'000'000).
        /// </summary>
        public Faker<CostCenterId> BuildCostCenterIdFaker()
        {
            var result = GetFaker(() => new Faker<CostCenterId>()
                .CustomInstantiator(f => new CostCenterId(f.Random.Number(1, 1_000_000))));
            return result;
        }

        /// <summary>
        /// A random cost center name faker.
        /// </summary>
        public Faker<CostCenterName> BuildCostCenterNameFaker()
        {
            var result = GetFaker(() => new Faker<CostCenterName>()
                .CustomInstantiator(_ => (CostCenterName)_commerceFakerBuilder.BuildDepartmentFaker(1).Generate().Value ));
            return result;
        }

        /// <summary>
        /// A random enterprise name faker.
        /// {CompanyName} {CompanySuffix}
        /// </summary>
        /// <remarks>Example: Metropolitan Inc</remarks>
        public Faker<EnterpriseName> BuildEnterpriseNameFaker()
        {
            var result = GetFaker(() => new Faker<EnterpriseName>()
                .CustomInstantiator(_ => (EnterpriseName)BuildCompanyNameFaker(0).Generate().Value));
            return result;
        }

        /// <summary>
        /// A random job area expertise faker.
        /// </summary>
        public Faker<JobArea> BuildJobAreaFaker()
        {
            var result = GetFaker(() => new Faker<JobArea>()
                .CustomInstantiator(f => new JobArea(f.Name.JobArea())));
            return result;
        }

        /// <summary>
        /// A random job descriptor faker.
        /// </summary>
        public Faker<JobDescriptor> BuildJobDescriptorFaker()
        {
            var result = GetFaker(() => new Faker<JobDescriptor>()
                .CustomInstantiator(f => new JobDescriptor(f.Name.JobDescriptor())));
            return result;
        }

        /// <summary>
        /// A random job key faker.
        /// </summary>
        public Faker<JobKey> BuildJobKeyFaker()
        {
            var result = GetFaker(() => new Faker<JobKey>()
                .CustomInstantiator(f => new JobKey(f.Address.ZipCode("??#####"))));
            return result;
        }

        /// <summary>
        /// A random job title faker.
        /// </summary>
        public Faker<JobTitle> BuildJobTitleFaker()
        {
            var result = GetFaker(() => new Faker<JobTitle>()
                .CustomInstantiator(f => new JobTitle(f.Name.JobType())));
            return result;
        }

        /// <summary>
        /// A random job type faker.
        /// </summary>
        public Faker<JobType> BuildJobTypeFaker()
        {
            var result = GetFaker(() => new Faker<JobType>()
                .CustomInstantiator(f => new JobType(f.Name.JobType())));
            return result;
        }

        /// <summary>
        /// A random site code faker.
        /// </summary>
        public Faker<SiteCode> BuildSiteCodeFaker()
        {
            var result = GetFaker(() => new Faker<SiteCode>()
                .CustomInstantiator(f =>
                {
                    var city = f.Address.City();
                    var cityCode = city.Length > 3 ? city[..3] : city;
                    var siteNumber = f.Random.UInt(1, 16);
                    return new SiteCode($"{cityCode.ToUpper()}{siteNumber}");
                }));
            return result;
        }
    }
}