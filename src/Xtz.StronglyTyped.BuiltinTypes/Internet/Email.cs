using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    /// <summary>
    /// Email address.
    /// </summary>
    [StrongType(typeof(MailAddress))]
    public partial class Email : IHasMailAddress
    {
        public static readonly IReadOnlyCollection<char> ILLEGAL_CHARS = new[] { ' ', '<', '>' };

        internal static readonly string ILLEGAL_CHARS_STRING = $"'{string.Join("', '", ILLEGAL_CHARS)}";

        public Email(string value)
            : this(new MailAddress(value))
        {
            if (value.Any(c => ILLEGAL_CHARS.Contains(c))) Throw($"Characters {ILLEGAL_CHARS_STRING} are not allowed in email");
        }
    }
}
