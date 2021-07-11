namespace Xtz.StronglyTyped.SourceGenerator
{
    public class InnerTypeHasStringConstructor
    {
        public bool Value { get; }

        public InnerTypeHasStringConstructor(bool value)
        {
            Value = value;
        }

        public static explicit operator InnerTypeHasStringConstructor(bool value) => new(value);

        public static implicit operator bool(InnerTypeHasStringConstructor? stronglyTyped) => stronglyTyped?.Value ?? false;
    }
}