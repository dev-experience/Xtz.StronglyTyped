using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
    public record ExtraFeatures(
        IsAbstract IsAbstract,
        HasBaseClass HasBaseClass,
        DoesAllowEmpty DoesAllowEmpty,
        DoGenerateStringConstructor DoGenerateStringConstructor,
        HasToString HasToString,
        HasIsValid HasIsValid);
}