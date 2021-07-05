using System;
using System.Text;

namespace Xtz.StronglyTyped.SourceGenerator
{
    public interface ICodeWriter
    {
        StringBuilder Content { get; }

        int IndentLevel { get; set; }

        void Append(string text);

        void AppendLine(string line);

        void AppendLine();

        IDisposable BeginScope(string line);

        IDisposable BeginScope();

        void EndScope();

        void StartLine();

        void EndLine();
    }
}