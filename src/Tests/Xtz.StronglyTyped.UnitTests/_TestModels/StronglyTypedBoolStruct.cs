using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.UnitTests
{
    // TODO: Remove `Allow.Empty` when it's supported by generator
    [StrongType(typeof(bool), Allow.Empty)]
    public partial struct StronglyTypedBoolStruct
    {
    }
}