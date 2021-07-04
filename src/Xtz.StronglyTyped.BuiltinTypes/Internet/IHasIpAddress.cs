using System.Net;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    public interface IHasIpAddress
    {
        IPAddress Value { get; }
    }
}