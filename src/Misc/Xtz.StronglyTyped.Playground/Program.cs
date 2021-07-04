using System;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;

namespace Xtz.StronglyTyped.Playground
{
    // NOTE: Just a simple console app for demo purposes

    public class Program
    {
        public static void Main()
        {
            var numberFaker = new NumbersFakerBuilder();

            // NOTE: Generate random strongly-typed negative `Int32` number
            var negativeInt = numberFaker.BuildNegativeInt32Faker(-1, 7).Generate();
            Console.WriteLine(negativeInt);

            // NOTE: Generate random strongly-typed negative `Int32` number
            var negativeInt2 = numberFaker.BuildNegativeInt32Faker(-567, 7).Generate();
            Console.WriteLine(negativeInt2);

            RunAddressFakers();
            RunInternetFakers();
        }

        private static void RunAddressFakers()
        {
            var fakerBuilder = new AddressFakerBuilder(true);

            var city = fakerBuilder.BuildCityFaker().Generate();
            Console.WriteLine(city);

            var cityPrefix = fakerBuilder.BuildCityPrefixFaker().Generate();
            Console.WriteLine(cityPrefix);

            var citySuffix = fakerBuilder.BuildCitySuffixFaker().Generate();
            Console.WriteLine(citySuffix);

            var country = fakerBuilder.BuildCountryFaker().Generate();
            Console.WriteLine(country);

            var countryCode = fakerBuilder.BuildCountryCodeFaker().Generate();
            Console.WriteLine(countryCode);

            var county = fakerBuilder.BuildCountyFaker().Generate();
            Console.WriteLine(county);

            var fullAddress = fakerBuilder.BuildFullAddressFaker().Generate();
            Console.WriteLine(fullAddress);

            var secondaryAddress = fakerBuilder.BuildSecondaryAddressFaker().Generate();
            Console.WriteLine(secondaryAddress);

            var state = fakerBuilder.BuildStateFaker().Generate();
            Console.WriteLine(state);

            var stateAbbr = fakerBuilder.BuildStateAbbrFaker().Generate();
            Console.WriteLine(stateAbbr);

            var streetAddress = fakerBuilder.BuildStreetAddressFaker().Generate();
            Console.WriteLine(streetAddress);

            var streetName = fakerBuilder.BuildStreetNameFaker().Generate();
            Console.WriteLine(streetName);

            var streetSuffix = fakerBuilder.BuildStreetSuffixFaker().Generate();
            Console.WriteLine(streetSuffix);

            var postalCode = fakerBuilder.BuildPostalCodeFaker().Generate();
            Console.WriteLine(postalCode);
        }

        private static void RunInternetFakers()
        {
            var fakerBuilder = new InternetFakerBuilder(true);

            var absoluteUri = fakerBuilder.BuildAbsoluteUriFaker().Generate();
            Console.WriteLine(absoluteUri);

            var avatarUri = fakerBuilder.BuildAvatarUriFaker().Generate();
            Console.WriteLine(avatarUri);

            var domainName = fakerBuilder.BuildDomainNameFaker().Generate();
            Console.WriteLine(domainName);

            var email = fakerBuilder.BuildEmailFaker().Generate();
            Console.WriteLine(email);

            var exampleEmail = fakerBuilder.BuildExampleEmailFaker().Generate();
            Console.WriteLine(exampleEmail);

            var ipV4Address = fakerBuilder.BuildIpV4AddressFaker().Generate();
            Console.WriteLine(ipV4Address);

            var ipV6Address = fakerBuilder.BuildIpV6AddressFaker().Generate();
            Console.WriteLine(ipV6Address);

            var macAddress = fakerBuilder.BuildMacAddressFaker().Generate();
            Console.WriteLine(macAddress);

            var portNumber = fakerBuilder.BuildPortNumberFaker().Generate();
            Console.WriteLine(portNumber);

            var relativeUri = fakerBuilder.BuildRelativeUriFaker().Generate();
            Console.WriteLine(relativeUri);

            var topLevelDomain = fakerBuilder.BuildTopLevelDomainFaker().Generate();
            Console.WriteLine(topLevelDomain);

            var userAgent = fakerBuilder.BuildUserAgentFaker().Generate();
            Console.WriteLine(userAgent);

            var userName = fakerBuilder.BuildUserNameFaker().Generate();
            Console.WriteLine(userName);
        }
    }
}
