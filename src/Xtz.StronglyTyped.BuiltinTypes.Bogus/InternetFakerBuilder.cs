using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Internet;
using Xtz.StronglyTyped.BuiltinTypes.Name;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class InternetFakerBuilder : BaseFakerBuilder
    {
        public InternetFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// An absolute URL with a random path faker.
        /// </summary>
        /// <param name="protocol">Protocol part of the URL, random if null</param>
        /// <param name="domain">Domain part of the URL, random if null</param>
        /// <param name="fileExt">The file extension to use in the path, directory if null</param>
        public Faker<AbsoluteUri> BuildAbsoluteUriFaker(WebsiteProtocol? protocol = null, DomainName? domain = null, string? fileExt = null)
        {
            var cacheKey = $"{protocol}|{domain}|{fileExt}";

            var result = GetFaker(() => new Faker<AbsoluteUri>()
                .CustomInstantiator(f => new AbsoluteUri(f.Internet.UrlWithPath(protocol, domain, fileExt))), cacheKey);
            return result;
        }

        /// <summary>
        /// A legit avatar URL (from twitter accounts) faker.
        /// </summary>
        public Faker<AvatarUri> BuildAvatarUriFaker()
        {
            var result = GetFaker(() => new Faker<AvatarUri>()
                .CustomInstantiator(f => new AvatarUri(f.Internet.Avatar())));
            return result;
        }

        /// <summary>
        /// A random domain name faker (2nd-level domains).
        /// </summary>
        public Faker<DomainName> BuildDomainNameFaker()
        {
            var result = GetFaker(() => new Faker<DomainName>()
                .CustomInstantiator(f => new DomainName(f.Internet.DomainName())));
            return result;
        }

        /// <summary>
        /// A random email address faker.
        /// </summary>
        /// <param name="firstName">Always use this first name.</param>
        /// <param name="lastName">Sometimes used depending on randomness. See 'UserName'.</param>
        /// <param name="provider">Always use the provider.</param>
        /// <param name="uniqueSuffix">This parameter is appended to
        /// the email account just before the @ symbol. This is useful for situations
        /// where you might have a unique email constraint in your database or application.
        /// Passing var f = new Faker(); f.UniqueIndex is a good choice. Or you can supply
        /// your own unique changing suffix too like Guid.NewGuid; just be sure to change the
        /// <paramref name="uniqueSuffix"/> value each time before calling this method
        /// to ensure that email accounts that are generated are totally unique.</param>
        public Faker<Email> BuildEmailFaker(FirstName? firstName = null, LastName? lastName = null, DomainName? provider = null, string? uniqueSuffix = null)
        {
            var cacheKey = $"{firstName}|{lastName}|{provider}|{uniqueSuffix}";

            var result = GetFaker(() => new Faker<Email>()
                .CustomInstantiator(f => new Email(f.Internet.Email(firstName, lastName, provider, uniqueSuffix))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random example email address (@example.com) faker.
        /// </summary>
        /// <param name="firstName">Optional: first name of the user.</param>
        /// <param name="lastName">Optional: last name of the user.</param>
        public Faker<ExampleEmail> BuildExampleEmailFaker(FirstName? firstName = null, LastName? lastName = null)
        {
            var cacheKey = $"{firstName}|{lastName}";

            var result = GetFaker(() => new Faker<ExampleEmail>()
                .CustomInstantiator(f => new ExampleEmail(f.Internet.ExampleEmail(firstName, lastName))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random IPv4 IP address faker.
        /// </summary>
        public Faker<IpV4Address> BuildIpV4AddressFaker()
        {
            var result = GetFaker(() => new Faker<IpV4Address>()
                .CustomInstantiator(f => new IpV4Address(f.Internet.IpAddress())));
            return result;
        }

        /// <summary>
        /// A random IPv6 IP address faker.
        /// </summary>
        public Faker<IpV6Address> BuildIpV6AddressFaker()
        {
            var result = GetFaker(() => new Faker<IpV6Address>()
                .CustomInstantiator(f => new IpV6Address(f.Internet.Ipv6Address())));
            return result;
        }

        /// <summary>
        /// A random mac address faker.
        /// </summary>
        public Faker<MacAddress> BuildMacAddressFaker()
        {
            var result = GetFaker(() => new Faker<MacAddress>()
                .CustomInstantiator(f => new MacAddress(f.Internet.Mac(string.Empty))));
            return result;
        }

        /// <summary>
        /// A random port number faker.
        /// </summary>
        public Faker<PortNumber> BuildPortNumberFaker()
        {
            var result = GetFaker(() => new Faker<PortNumber>()
                .CustomInstantiator(f => new PortNumber(f.Internet.Port())));
            return result;
        }

        /// <summary>
        /// A rooted URL path (with optional file extension) faker.
        /// </summary>
        /// <remarks>Example: /foo/bar</remarks>
        /// <param name="fileExt">Optional: The file extension to use. If <paramref name="fileExt"/> is null, then a rooted URL directory is returned.</param>
        public Faker<RelativeUri> BuildRelativeUriFaker(string? fileExt = null)
        {
            var result = GetFaker(() => new Faker<RelativeUri>()
                .CustomInstantiator(f => new RelativeUri(f.Internet.UrlRootedPath(fileExt))), fileExt);
            return result;
        }

        /// <summary>
        /// A random top-level domain faker.
        /// </summary>
        /// <remarks>Example: ".com", ".net", ".org", etc.</remarks>
        public Faker<TopLevelDomain> BuildTopLevelDomainFaker()
        {
            var result = GetFaker(() => new Faker<TopLevelDomain>()
                .CustomInstantiator(f => new TopLevelDomain(f.Internet.DomainSuffix())));
            return result;
        }

        /// <summary>
        /// A random user agent faker.
        /// </summary>
        public Faker<UserAgent> BuildUserAgentFaker()
        {
            var result = GetFaker(() => new Faker<UserAgent>()
                .CustomInstantiator(f => new UserAgent(f.Internet.UserAgent())));
            return result;
        }

        /// <summary>
        /// A random user name faker.
        /// </summary>
        /// <param name="firstName">First name is always part of the returned user name.</param>
        /// <param name="lastName">Last name may or may not be used.</param>
        public Faker<UserName> BuildUserNameFaker(FirstName? firstName = null, LastName? lastName = null)
        {
            var cacheKey = $"{firstName}|{lastName}";

            var result = GetFaker(() => new Faker<UserName>()
                .CustomInstantiator(f => new UserName(f.Internet.UserName(firstName, lastName))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random website protocol faker.
        /// </summary>
        /// <remarks>Only: HTTP or HTTPS</remarks>
        public Faker<WebsiteProtocol> BuildWebsiteProtocolFaker()
        {
            var result = GetFaker(() => new Faker<WebsiteProtocol>()
                .CustomInstantiator(f => new WebsiteProtocol(f.Internet.Protocol())));
            return result;
        }
    }
}
