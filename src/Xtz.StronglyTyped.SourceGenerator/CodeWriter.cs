using System;
using System.Text;

namespace Xtz.StronglyTyped.SourceGenerator
{
    public class CodeWriter : ICodeWriter
    {
        private readonly ScopeTracker _scopeTracker;

        public StringBuilder Content { get; } = new();

        public int IndentLevel { get; set; }

        public CodeWriter()
        {
            // We only need one. It can be reused.
            _scopeTracker = new ScopeTracker(this);
        }

        public void Append(string text) => Content.Append(text);

        public void AppendLine(string line) => Content.Append(new string(' ', IndentLevel * 4)).AppendLine(line);

        public void AppendLine() => Content.AppendLine();

        public IDisposable BeginScope(string line)
        {
            AppendLine(line);
            return BeginScope();
        }

        public IDisposable BeginScope()
        {
            // TODO: Replace by pre-built array of spaces. Do substring
            Content.Append(new string(' ', IndentLevel * 4)).AppendLine("{");
            IndentLevel += 1;
            return _scopeTracker;
        }

        public void EndScope()
        {
            IndentLevel -= 1;
            Content.Append(new string(' ', IndentLevel * 4)).AppendLine("}");
        }

        public void StartLine() => Content.Append(new string(' ', IndentLevel * 4));

        public void EndLine() => Content.AppendLine();

        public override string ToString() => Content.ToString();

        public class ScopeTracker : IDisposable
        {
            private readonly CodeWriter _parent;
            
            public ScopeTracker(CodeWriter parent)
            {
                _parent = parent;
            }

            public void Dispose()
            {
                _parent.EndScope();
            }
        }
    }
}