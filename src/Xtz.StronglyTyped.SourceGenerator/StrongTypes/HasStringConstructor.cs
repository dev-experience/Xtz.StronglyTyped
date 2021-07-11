namespace Xtz.StronglyTyped.SourceGenerator
{
    public class HasStringConstructor
    {
        public bool Value { get; }

        public HasStringConstructor(bool value)
        {
            Value = value;
        }

        public static explicit operator HasStringConstructor(bool value) => new(value);

        public static implicit operator bool(HasStringConstructor? stronglyTyped) => stronglyTyped?.Value ?? false;
    }
}