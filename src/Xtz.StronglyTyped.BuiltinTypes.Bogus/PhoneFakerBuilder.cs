using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Phone;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class PhoneFakerBuilder : BaseFakerBuilder
    {
        public PhoneFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// A random phone number faker.
        /// </summary>
        /// <param name="format">
        /// Format of phone number in any format.
        /// Replaces # characters with numbers. IE: '###-###-####' or '(###) ###-####'.
        /// </param>
        public Faker<PhoneNumber> BuildPhoneNumberFaker(string? format = null)
        {
            var result = GetFaker(() => new Faker<PhoneNumber>()
                .CustomInstantiator(f => new PhoneNumber(f.Phone.PhoneNumber(format))), format);
            return result;
        }
    }
}