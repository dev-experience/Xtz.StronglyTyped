namespace Xtz.StronglyTyped.Benchmark.Models
{
    // TODO: Support calling `.ThrowIfInvalid()` in struct constructors
    [StrongType(typeof(int))]
    public partial struct IntStructId
    {
        private void ThrowIfInvalid(int value)
        {
            // ID must be greater than 0
            if (value <= 0)
            {
                throw new StronglyTypedException(GetType(), $"'{value}' value is invalid");
            }
        }
    }
}