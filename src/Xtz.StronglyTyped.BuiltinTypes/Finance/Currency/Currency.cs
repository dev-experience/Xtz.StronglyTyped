namespace Xtz.StronglyTyped.BuiltinTypes.Finance
{
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public record Currency(CurrencyName Name, CurrencyCode Code, CurrencySymbol? Symbol);

}