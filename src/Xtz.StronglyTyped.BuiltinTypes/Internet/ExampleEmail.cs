using System.Linq;
using System.Net.Mail;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    /// <summary>
    /// Email address (@example.com).
    /// </summary>
    [StrongType(typeof(MailAddress))]
    public partial class ExampleEmail : IHasMailAddress
    {
        public ExampleEmail(string value)
            : base(new MailAddress(value))
        {
            if (value.Any(c => Email.IllegalChars.Contains(c))) Throw($"Characters {Email.IllegalCharsString} are not allowed in email");
        }
    }
}