using System;
using Xtz.StronglyTyped.BuiltinTypes.Ids;
using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.UnitTests
{
    [StrongType(typeof(Guid))]
    public partial class StronglyTypedGuidId : GuidId
    {
    }
}