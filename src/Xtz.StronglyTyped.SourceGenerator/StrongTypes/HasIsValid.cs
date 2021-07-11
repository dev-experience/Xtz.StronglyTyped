using System.Diagnostics;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [DebuggerDisplay("{Value}")]
    public class HasIsValid
    {
        public bool Value { get; }

        public HasIsValid(bool value)
        {
            Value = value;
        }

        public static explicit operator HasIsValid(bool value) => new(value);

        public static implicit operator bool(HasIsValid? stronglyTyped) => stronglyTyped?.Value ?? false;
    }
}