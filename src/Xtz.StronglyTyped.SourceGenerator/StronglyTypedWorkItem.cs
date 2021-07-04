using System;

namespace Xtz.StronglyTyped.SourceGenerator
{
    public record StronglyTypedWorkItem(
        StrongTypeDeclaration TypeDeclarationSyntax,
        WorkItemKind Kind,
        string Namespace,
        string TypeName,
        Type InnerType,
        bool HasBaseType,
        ExtraFeatures ExtraFeatures);
}