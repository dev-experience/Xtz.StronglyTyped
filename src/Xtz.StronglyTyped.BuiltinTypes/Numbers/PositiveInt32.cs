using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Positive (greater than zero) <see cref="System.Int32"/> number.
    /// </summary>
    [StrongType(typeof(int))]
    public partial class PositiveInt32
    {
        protected override bool IsValid(int value)
        {
            return value > 0;
        }
    }
}