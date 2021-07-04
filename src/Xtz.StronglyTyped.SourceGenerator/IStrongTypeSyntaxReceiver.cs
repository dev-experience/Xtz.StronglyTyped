using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Xtz.StronglyTyped.SourceGenerator
{
    public interface IStrongTypeSyntaxReceiver : ISyntaxReceiver
    {
        IReadOnlyCollection<string> Log { get; }

        IReadOnlyCollection<StrongTypeDeclaration> Declarations { get; }

        bool IsStrongTypeAttribute(AttributeSyntax attributeSyntax);
    }
}