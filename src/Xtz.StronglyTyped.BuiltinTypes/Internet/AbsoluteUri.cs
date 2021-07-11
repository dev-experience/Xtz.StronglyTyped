using System;
using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    /// <summary>
    /// Absolute URI.
    /// </summary>
    [StrongType(typeof(Uri))]
    public partial class AbsoluteUri
    {
        public AbsoluteUri(string value)
            : base(new Uri(value, UriKind.Absolute))
        {
        }

        protected override bool IsValid(Uri value)
        {
            return value.IsAbsoluteUri;
        }
    }
}
