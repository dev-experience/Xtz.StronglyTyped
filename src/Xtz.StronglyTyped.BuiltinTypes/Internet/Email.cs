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
        public static readonly IReadOnlyCollection<char> IllegalChars = new[] { ' ', '<', '>' };

        internal static readonly string IllegalCharsString = $"'{string.Join("', '", IllegalChars)}";

        public Email(string value)
            : this(new MailAddress(value))
        {
            if (value.Any(c => IllegalChars.Contains(c))) Throw($"Characters {IllegalCharsString} are not allowed in email");
        }
    }
}
