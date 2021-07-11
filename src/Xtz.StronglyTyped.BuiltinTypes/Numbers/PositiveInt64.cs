using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Positive (greater than zero) <see cref="System.Int64"/> number.
    /// </summary>
    [StrongType(typeof(long))]
    public partial class PositiveInt64
    {
        protected override bool IsValid(long value)
        {
            return value > 0;
        }
    }
}