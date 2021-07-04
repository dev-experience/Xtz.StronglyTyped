namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Odd (non-even) <see cref="System.Int64"/> number.
    /// </summary>
    [StrongType(typeof(long))]
    public partial class OddInt64
    {
        protected override bool IsValid(long value)
        {
            return value % 2 == 1;
        }
    }
}