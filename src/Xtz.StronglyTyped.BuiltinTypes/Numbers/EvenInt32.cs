using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Even <see cref="System.Int32"/> number.
    /// </summary>
    [StrongType(typeof(int), Allow.Empty)]
    public partial class EvenInt32
    {
        protected override bool IsValid(int value)
        {
            return value % 2 == 0;
        }
    }
}
