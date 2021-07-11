using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Event <see cref="System.Int64"/> number.
    /// </summary>
    [StrongType(typeof(long), Allow.Empty)]
    public partial class EvenInt64
    {
        protected override bool IsValid(long value)
        {
            return value % 2 == 0;
        }
    }
}