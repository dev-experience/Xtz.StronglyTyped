using System.Net;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    /// <summary>
    /// IP v6 address.
    /// </summary>
    [StrongType(typeof(IPAddress))]
    public partial class IpV6Address : IHasIpAddress
    {
        public IpV6Address(string value)
            : base(IPAddress.Parse(value))
        {
        }

        public IpV6Address(byte[] value)
            : base(new IPAddress(value))
        {
        }

        public static bool IsIpv6Address(IPAddress value)
        {
            return value.IsIPv6LinkLocal || value.IsIPv6Multicast || value.IsIPv6SiteLocal || value.IsIPv6Teredo
                || value.IsIPv4MappedToIPv6 || value.GetAddressBytes().Length > 4;
        }
    }
}