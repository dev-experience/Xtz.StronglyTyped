using System.Diagnostics;

namespace Xtz.StronglyTyped.BuiltinTypes.Commerce
{
    /// <summary>
    /// Product full name.
    /// </summary>
    /// <remarks>{Adjective} {Material} {Name}</remarks>
    [DebuggerDisplay("{ToString(),nq}")]
    public record ProductFullName(ProductAdjective ProductAdjective, ProductMaterial ProductMaterial, ProductShortName ProductShortName)
    {
        public override string ToString() => $"{ProductAdjective} {ProductMaterial} {ProductShortName}";
    }
}