using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.UnitTests
{
    [StrongType(typeof(int))]
    public partial class CountryId
    {
        protected override bool IsValid(int value)
        {
            // ID must be greater than 0
            return value > 0;
        }
    }
}
