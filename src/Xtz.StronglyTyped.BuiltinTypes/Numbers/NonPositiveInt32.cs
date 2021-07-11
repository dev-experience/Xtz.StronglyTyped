using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Non-positive (equal or less than zero) <see cref="System.Int32"/> number.
    /// </summary>
    [StrongType(typeof(int), Allow.Empty)]
    public partial class NonPositiveInt32
    {
        protected override bool IsValid(int value)
        {
            return value <= 0;
        }
    }
}