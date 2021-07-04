namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Non-negative (equal or greater than zero) <see cref="System.Int64"/> number.
    /// </summary>
    [StrongType(typeof(long))]
    public partial class NonNegativeInt64
    {
        protected override bool IsValid(long value)
        {
            return value >= 0;
        }
    }
}