using System.Collections.Generic;
using NUnit.Framework;
using Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions;
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

            Assert.That(values, Is.All.Matches<object>(x => !x.ToString()!.IsBogusGeneratedValue()));
        }

        [Test]
        [StrongAutoData]
        public void ShouldGenerateStronglyTypedCollection(IEnumerable<Email> values)
        {
            Assert.IsNotEmpty(values);
        }
    }
}