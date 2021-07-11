using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Company;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class CompanyAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            CompanyName companyName,
            CompanySuffix companySuffix,
            CostCenterId costCenterId,
            CostCenterName costCenterName,
            EnterpriseName enterpriseName,
            JobArea jobArea,
            JobDescriptor jobDescriptor,
            JobKey jobKey,
            JobTitle jobTitle,
            JobType jobType,
            SiteCode siteCode)
        {
            var values = new object[]
            {
                companyName,
                companySuffix,
                costCenterId,
                costCenterName,
                enterpriseName,
                jobArea,
                jobDescriptor,
                jobKey,
                jobTitle,
                jobType,
                siteCode
            };

            var nonBogusValues = values
                .Where(x => x.ToString()!.Length >= 36 && Guid.TryParse(x.ToString()![^36..], out _));

            Assert.IsEmpty(nonBogusValues);
        }
    }
}