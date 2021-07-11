using System.Diagnostics;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [DebuggerDisplay("{Value}")]
    public class DoesAllowEmpty
    {
        public bool Value { get; }

        public DoesAllowEmpty(bool value)
        {
            Value = value;
        }

        public static explicit operator DoesAllowEmpty(bool value) => new(value);

        public static implicit operator bool(DoesAllowEmpty? stronglyTyped) => stronglyTyped?.Value ?? false;
    }
}