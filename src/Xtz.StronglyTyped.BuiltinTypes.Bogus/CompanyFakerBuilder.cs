using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Company;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class CompanyFakerBuilder : BaseFakerBuilder
    {
        public CompanyFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
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
        /// A random enterprise name faker.
        /// {CompanyName} {CompanySuffix}
        /// </summary>
        /// <remarks>Example: Metropolitan Inc</remarks>
        public Faker<EnterpriseName> BuildEnterpriseNameFaker()
        {
            var result = GetFaker(() => new Faker<EnterpriseName>()
                .CustomInstantiator(_ => new EnterpriseName(BuildCompanyNameFaker(0).Generate().ToString())));
            return result;
        }
    }
}