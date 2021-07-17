using System;
using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    // TODO: Vlad DX: Think about using `AbsoluteUri` as inner type (could be not the best idea to use strong type in strong type)
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