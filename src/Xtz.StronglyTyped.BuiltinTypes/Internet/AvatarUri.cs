using System;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    // TODO: Use `AbsoluteUri` as inner type
    /// <summary>
    /// Absolute avatar URL.
    /// </summary>
    [StrongType(typeof(Uri))]
    public partial class AvatarUri
    {
        public AvatarUri(string value)
            : base(new Uri(value, UriKind.Absolute))
        {
        }

        protected override bool IsValid(Uri value)
        {
            return value.IsAbsoluteUri && (string.Equals(value.Scheme, "https", StringComparison.InvariantCultureIgnoreCase) || string.Equals(value.Scheme, "http", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}