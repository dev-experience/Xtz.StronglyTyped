namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Negative (less than zero) <see cref="System.Int32"/> number.
    /// </summary>
    [StrongType(typeof(int))]
    public partial class NegativeInt32
    {
        protected override bool IsValid(int value)
        {
            return value < 0;
        }
    }
}