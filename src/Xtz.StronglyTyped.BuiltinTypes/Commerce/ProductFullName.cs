namespace Xtz.StronglyTyped.BuiltinTypes.Commerce
{
    // TODO: Add `[DebuggerDisplay]` and `.ToString()` to all records
    /// <summary>
    /// Product full name.
    /// </summary>
    /// <remarks>{Adjective} {Material} {Name}</remarks>
    public record ProductFullName(ProductAdjective ProductAdjective, ProductMaterial ProductMaterial, ProductShortName ProductShortName);
}