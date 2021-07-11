namespace Xtz.StronglyTyped
{
    public interface IValidatableStruct<in TInnerType>
    {
        bool IsValid(TInnerType value);
    }
}