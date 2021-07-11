using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Negative (less than zero) <see cref="System.Int64"/> number.
    /// </summary>
    [StrongType(typeof(long))]
    public partial class NegativeInt64
    {
        protected override bool IsValid(long value)
        {
            return value < 0;
        }
    }
}