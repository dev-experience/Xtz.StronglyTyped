using System.Diagnostics;

namespace Xtz.StronglyTyped.BuiltinTypes.Finance
{
    /// <summary>
    /// Represents a currency.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public record Currency(CurrencyName Name, CurrencyCode Code, CurrencySymbol? Symbol)
    {
        public override string ToString() => $"{Name} ({Code}, symbol: {Symbol ?? "<none>"})";
    }
}