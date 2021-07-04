using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator
{
    public interface IDataExtractor
    {
        IReadOnlyCollection<string> Log { get; }

        bool BuildWorkItem(
            SemanticModel semanticModel,
            SyntaxReceiver receiver,
            StrongTypeDeclaration declaration,
            out StronglyTypedWorkItem? workItem);
    }
}