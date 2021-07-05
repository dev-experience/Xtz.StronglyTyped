using System.Net;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    /// <summary>
    /// IP v4 address.
    /// </summary>
    [StrongType(typeof(IPAddress))]
    public partial class IpV4Address : IHasIpAddress
    {
        public IpV4Address(byte[] value)
            : base(new IPAddress(value))
        {
        }

        protected override bool IsValid(IPAddress value)
        {
            return !IpV6Address.IsIpv6Address(value);
        }
    }
}