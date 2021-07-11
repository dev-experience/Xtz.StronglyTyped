using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.UnitTests
{
    [StrongType(typeof(int))]
    public partial struct UserStructIntId : IValidatableStruct<int>
    {
        public bool IsValid(int value)
        {
            // ID must be greater than 0
            return value > 0;
        }
    }
}
