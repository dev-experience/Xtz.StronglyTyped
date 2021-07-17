using System.Diagnostics;
using Xtz.StronglyTyped.BuiltinTypes.Finance;

namespace Xtz.StronglyTyped.BuiltinTypes.Commerce
{
    /// <summary>
    /// Price.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public record Price(Amount Amount, Currency Currency)
    {
        public override string ToString() => $"{Amount} {Currency.Code}";
    }
}