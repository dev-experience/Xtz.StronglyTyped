using System.Net.Mail;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    public interface IHasMailAddress
    {
        MailAddress Value { get; }
    }
}