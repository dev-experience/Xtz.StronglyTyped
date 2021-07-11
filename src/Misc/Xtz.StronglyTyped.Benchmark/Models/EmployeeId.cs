using System;
using Xtz.StronglyTyped.BuiltinTypes.Ids;
using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.Benchmark.Models
{
    [StrongType(typeof(Guid))]
    public partial class EmployeeGuidId : GuidId
    {
    }
}
