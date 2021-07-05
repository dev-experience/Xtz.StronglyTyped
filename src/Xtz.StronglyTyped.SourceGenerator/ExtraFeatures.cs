using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
    public record ExtraFeatures(
        bool IsAbstract,
        bool HasBaseType,
        bool DoGenerateStringConstructor,
        bool HasToString,
        bool HasIsValid);
}