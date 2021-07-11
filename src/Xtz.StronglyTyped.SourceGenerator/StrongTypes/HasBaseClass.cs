using System.Diagnostics;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [DebuggerDisplay("{Value}")]
    public class HasBaseClass
    {
        public bool Value { get; }

        public HasBaseClass(bool value)
        {
            Value = value;
        }

        public static explicit operator HasBaseClass(bool value) => new(value);

        public static implicit operator bool(HasBaseClass? stronglyTyped) => stronglyTyped?.Value ?? false;
    }
}