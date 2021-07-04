using System;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    /// <summary>
    /// Relative URI.
    /// </summary>
    [StrongType(typeof(Uri))]
    public partial class RelativeUri
    {
        public RelativeUri(string value)
            : base(new Uri(value, UriKind.Relative))
        {
        }

        protected override bool IsValid(Uri value)
        {
            return !value.IsAbsoluteUri;
        }
    }
}