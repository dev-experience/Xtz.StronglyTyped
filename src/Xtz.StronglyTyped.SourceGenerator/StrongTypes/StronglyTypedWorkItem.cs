using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
    public record StronglyTypedWorkItem(
        StrongTypeDeclaration TypeDeclarationSyntax,
        WorkItemKind Kind,
        string? Namespace,
        string TypeName,
        Type InnerType,
        ExtraFeatures ExtraFeatures);
}