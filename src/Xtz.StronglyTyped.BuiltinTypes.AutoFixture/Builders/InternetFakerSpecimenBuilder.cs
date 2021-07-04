using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture.Builders
{
    public class InternetFakerSpecimenBuilder : BaseFakerSpecimenBuilder
    {
        private readonly InternetFakerBuilder _builder = new(true);

        protected override Dictionary<Type, Func<IFakerTInternal>> FakerFactories => new()
        {
            { typeof(AbsoluteUri), () => _builder.BuildAbsoluteUriFaker() },
            { typeof(AvatarUri), () => _builder.BuildAvatarUriFaker() },
            { typeof(DomainName), () => _builder.BuildDomainNameFaker() },
            { typeof(Email), () => _builder.BuildEmailFaker() },
            { typeof(ExampleEmail), () => _builder.BuildExampleEmailFaker() },
            { typeof(IpV4Address), () => _builder.BuildIpV4AddressFaker() },
            { typeof(IpV6Address), () => _builder.BuildIpV6AddressFaker() },
            { typeof(MacAddress), () => _builder.BuildMacAddressFaker() },
            { typeof(PortNumber), () => _builder.BuildPortNumberFaker() },
            { typeof(RelativeUri), () => _builder.BuildRelativeUriFaker() },
            { typeof(TopLevelDomain), () => _builder.BuildTopLevelDomainFaker() },
            { typeof(UserAgent), () => _builder.BuildUserAgentFaker() },
            { typeof(UserName), () => _builder.BuildUserNameFaker() },
            { typeof(WebsiteProtocol), () => _builder.BuildWebsiteProtocolFaker() },
        };
    }
}