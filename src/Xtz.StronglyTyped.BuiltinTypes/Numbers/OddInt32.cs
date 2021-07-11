using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Odd (non-even) <see cref="System.Int32"/> number.
    /// </summary>
    [StrongType(typeof(int), Allow.Empty)]
    public partial class OddInt32
    {
        protected override bool IsValid(int value)
        {
            return value % 2 == 1;
        }
    }
}