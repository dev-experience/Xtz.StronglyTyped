using System.Text.RegularExpressions;

namespace Xtz.StronglyTyped.BogusAutoFixture.UnitTests.Extensions
{
    public static class Extensions
    {
        /// <remarks>value0edafc90-698d-4804-9267-3c5e73f4aa94</remarks>
        private static readonly Regex BOGUS_VALUE_REGEX = new Regex("value[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsBogusGeneratedValue(this string value)
        {
            return BOGUS_VALUE_REGEX.IsMatch(value);
        }
    }
}
