using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
    public record ConstructorDescriptor(string TypeName, string ParsingExpression);
}