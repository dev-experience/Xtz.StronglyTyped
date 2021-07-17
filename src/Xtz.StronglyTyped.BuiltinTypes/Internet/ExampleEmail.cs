﻿using System.Linq;
using System.Net.Mail;
using Xtz.StronglyTyped.SourceGenerator;

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
            if (value.Any(c => Email.ILLEGAL_CHARS.Contains(c))) Throw($"Characters {Email.ILLEGAL_CHARS_STRING} are not allowed in email");
        }
    }
}