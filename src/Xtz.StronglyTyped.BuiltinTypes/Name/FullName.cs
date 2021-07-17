using System.Diagnostics;

namespace Xtz.StronglyTyped.BuiltinTypes.Name
{
    /// <summary>
    /// Full name.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public record FullName(FirstName FirstName, LastName LastName)
    {
        public override string ToString() => $"{FirstName} {LastName})";
    }
}