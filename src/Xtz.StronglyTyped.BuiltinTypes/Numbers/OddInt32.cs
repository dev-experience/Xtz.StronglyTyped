namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Odd (non-even) <see cref="System.Int32"/> number.
    /// </summary>
    [StrongType(typeof(int))]
    public partial class OddInt32
    {
        protected override bool IsValid(int value)
        {
            return value % 2 == 1;
        }
    }
}