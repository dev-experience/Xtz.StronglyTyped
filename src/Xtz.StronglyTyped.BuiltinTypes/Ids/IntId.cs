namespace Xtz.StronglyTyped.BuiltinTypes.Ids
{
    /// <summary>
    /// Base class for int-based IDs.
    /// </summary>
    public abstract class IntId : StronglyTyped<int>, IStronglyTypedId
    {
        protected IntId(int value)
            : base(value)
        {
        }
    }
}
