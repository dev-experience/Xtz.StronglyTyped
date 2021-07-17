using System;
using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    /// <summary>
    /// Website protocol.
    /// </summary>
    /// <remarks>Only: HTTP or HTTPS</remarks>
    [StrongType]
    public partial class WebsiteProtocol
    {
        /// <summary>
        /// HTTP protocol.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static readonly WebsiteProtocol HTTP = new WebsiteProtocol("http");

        /// <summary>
        /// HTTPS protocol.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static readonly WebsiteProtocol HTTPS = new WebsiteProtocol("https");

        protected override bool IsValid(string value)
        {
            return string.Equals(value, "https", StringComparison.InvariantCultureIgnoreCase) || string.Equals(value, "http", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}