namespace Xtz.StronglyTyped
{
    /// <summary>
    /// Interface to make inner values strongly-typed.
    /// </summary>
    /// <typeparam name="TInnerType">Inner type.</typeparam>
    public interface IStronglyTyped<out TInnerType> : IStronglyTyped
        where TInnerType : notnull
    {
        /// <summary>
        /// Inner value.
        /// </summary>
        TInnerType Value { get; }
    }
}
