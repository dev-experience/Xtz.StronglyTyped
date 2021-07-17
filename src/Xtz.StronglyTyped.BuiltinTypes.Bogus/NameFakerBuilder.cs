using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Name;
using Gender = Bogus.DataSets.Name.Gender;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class NameFakerBuilder : BaseFakerBuilder
    {
        public NameFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// A random display name faker.
        /// </summary>
        /// <remarks>A gender-specific name is only supported on locales that support it.</remarks>
        public Faker<DisplayName> BuildDisplayNameFaker(string locale = "en", Gender? gender = null)
        {
            var cacheKey = $"{locale}|{gender}";

            var result = GetFaker(() => new Faker<DisplayName>()
                .CustomInstantiator(_ =>
                {
                    var fullName = BuildFullNameFaker(locale, gender).Generate();
                    return new DisplayName($"{fullName.FirstName} {fullName.LastName}");
                }),
                cacheKey);
            return result;
        }

        /// <summary>
        /// A random first name faker.
        /// </summary>
        /// <remarks>A gender-specific name is only supported on locales that support it.</remarks>
        public Faker<FirstName> BuildFirstNameFaker(string locale = "en", Gender? gender = null)
        {
            var cacheKey = $"{locale}|{gender}";

            var result = GetFaker(() => new Faker<FirstName>()
                .CustomInstantiator(f => new FirstName(f.Name[locale].FirstName(gender))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random full name ({FirstName} {LastName}) faker.
        /// </summary>
        /// <remarks>A gender-specific name is only supported on locales that support it.</remarks>
        public Faker<FullName> BuildFullNameFaker(string locale = "en", Gender? gender = null)
        {
            var cacheKey = $"{locale}|{gender}";

            var result = GetFaker(() => new Faker<FullName>()
                .CustomInstantiator(
                    _ =>
                    {
                        var firstName = BuildFirstNameFaker(locale, gender).Generate();
                        var lastName = BuildLastNameFaker(locale, gender).Generate();
                        return new FullName(firstName, lastName);
                    }),
                    cacheKey);
            return result;
        }

        /// <summary>
        /// A random last name faker.
        /// </summary>
        /// <remarks>A gender-specific name is only supported on locales that support it.</remarks>
        public Faker<LastName> BuildLastNameFaker(string locale = "en", Gender? gender = null)
        {
            var cacheKey = $"{locale}|{gender}";

            var result = GetFaker(() => new Faker<LastName>()
                .CustomInstantiator(f => new LastName(f.Name[locale].LastName(gender))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random prefix for a name faker.
        /// </summary>
        /// <remarks>A gender-specific name is only supported on locales that support it.</remarks>
        public Faker<NamePrefix> BuildNamePrefixFaker(string locale = "en", Gender? gender = null)
        {
            var cacheKey = $"{locale}|{gender}";

            var result = GetFaker(() => new Faker<NamePrefix>()
                .CustomInstantiator(f => new NamePrefix(f.Name[locale].Prefix(gender))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random suffix for a name faker.
        /// </summary>
        public Faker<NameSuffix> BuildNameSuffixFaker(string locale = "en")
        {
            var cacheKey = $"{locale}";

            var result = GetFaker(() => new Faker<NameSuffix>()
                .CustomInstantiator(f => new NameSuffix(f.Name[locale].Suffix())), cacheKey);
            return result;
        }
    }
}