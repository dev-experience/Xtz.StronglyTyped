using System;
using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.EntityFramework.IntegrationTests.StrongTypes
{
    [StrongType(typeof(Guid))]
    public partial struct EmployeeStructGuidId
    {
        public static EmployeeStructGuidId New()
        {
            return new EmployeeStructGuidId(Guid.NewGuid());
        }
    }
}