namespace Xtz.StronglyTyped.BuiltinTypes.Ids
{
    /// <summary>
    /// Base class for int-based IDs.
    /// </summary>
    public abstract class IntId : StronglyTyped<int>
    {
        protected IntId(int value)
            : base(value)
        {
        }
    }
}
