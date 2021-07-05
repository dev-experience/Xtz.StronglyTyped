namespace Xtz.StronglyTyped.Benchmark.Models
{
    [StrongType(typeof(int))]
    public partial struct IntStructId
    {
        private bool IsValid(int value)
        {
            // ID must be greater than 0
            return value <= 0;
        }
    }
}