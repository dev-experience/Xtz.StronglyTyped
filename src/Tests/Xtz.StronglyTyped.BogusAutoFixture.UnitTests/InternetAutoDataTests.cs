using System;
using System.Linq;
using NUnit.Framework;
using Xtz.StronglyTyped.BuiltinTypes.AutoFixture;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests
{
    public class InternetAutoDataTests
    {
        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedValues(
            AbsoluteUri absoluteUri,
            AvatarUri avatarUri,
            DomainName domainName,
            Email email,
            ExampleEmail exampleEmail,
            IpV4Address ipV4Address,
            IpV6Address ipV6Address,
            MacAddress macAddress,
            PortNumber portNumber,
            RelativeUri relativeUri,
            TopLevelDomain topLevelDomain,
            UserAgent userAgent,
            UserName userName,
            WebsiteProtocol websiteProtocol)
        {
            var values = new object[]
            {
                absoluteUri,
                avatarUri,
                domainName,
                email,
                exampleEmail,
                ipV4Address,
                ipV6Address,
                macAddress,
                portNumber,
                relativeUri,
                topLevelDomain,
                userAgent,
                userName,
                websiteProtocol,
            };

            var nonBogusValues = values
                .Where(x => x.ToString()!.Length >= 36 && Guid.TryParse(x.ToString()![^36..], out _));

            Assert.IsEmpty(nonBogusValues);
        }
    }
}