using System.Diagnostics;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [DebuggerDisplay("{Value}")]
    public class IsAbstract
    {
        public bool Value { get; }

        public IsAbstract(bool value)
        {
            Value = value;
        }

        public static explicit operator IsAbstract(bool value) => new(value);

        public static implicit operator bool(IsAbstract? stronglyTyped) => stronglyTyped?.Value ?? false;
    }
}