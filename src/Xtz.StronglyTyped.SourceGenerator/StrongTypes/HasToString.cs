using System.Diagnostics;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [DebuggerDisplay("{Value}")]
    public class HasToString
    {
        public bool Value { get; }

        public HasToString(bool value)
        {
            Value = value;
        }
        public static explicit operator HasToString(bool value) => new(value);
        
        public static implicit operator bool(HasToString? stronglyTyped) => stronglyTyped?.Value ?? false;
    }
}