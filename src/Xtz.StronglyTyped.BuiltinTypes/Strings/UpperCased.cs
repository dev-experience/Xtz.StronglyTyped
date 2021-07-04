namespace Xtz.StronglyTyped.BuiltinTypes.Strings
{
    /// <summary>
    /// Upper-cased string.
    /// </summary>
    public class UpperCased : StronglyTyped<string>
    {
        public UpperCased(string value)
#pragma warning disable 8604
            : base(value?.ToUpperInvariant())
#pragma warning restore 8604
        {
        }
    }
}
