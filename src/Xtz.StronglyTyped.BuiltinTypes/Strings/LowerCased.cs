namespace Xtz.StronglyTyped.BuiltinTypes.Strings
{
    /// <summary>
    /// Lower-cased string.
    /// </summary>
    public class LowerCased : StronglyTyped<string>
    {
        public LowerCased(string? value)
#pragma warning disable 8604
            : base(value?.ToLowerInvariant())
#pragma warning restore 8604
        {
        }
    }
}