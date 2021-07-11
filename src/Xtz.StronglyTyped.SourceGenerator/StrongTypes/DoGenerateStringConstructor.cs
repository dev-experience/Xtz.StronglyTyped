using System.Diagnostics;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [DebuggerDisplay("{Value}")]
    public class DoGenerateStringConstructor
    {
        public bool Value { get; }

        public DoGenerateStringConstructor(bool value)
        {
            Value = value;
        }

        public static explicit operator DoGenerateStringConstructor(bool value) => new(value);

        public static implicit operator bool(DoGenerateStringConstructor? stronglyTyped) => stronglyTyped?.Value ?? false;
    }
}