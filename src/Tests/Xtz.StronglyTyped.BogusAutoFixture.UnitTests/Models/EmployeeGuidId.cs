using System;
using Xtz.StronglyTyped.BuiltinTypes.Ids;
using Xtz.StronglyTyped.SourceGenerator;

// ReSharper disable once CheckNamespace
// HACK: Normal namespace makes file-system path of the generated file too long
namespace UnitTests
{
    [StrongType(typeof(Guid))]
    public partial class EmployeeGuidId : GuidId
    {
    }
}