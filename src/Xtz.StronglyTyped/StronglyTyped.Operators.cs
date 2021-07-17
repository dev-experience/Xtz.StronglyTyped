using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    public abstract partial class StronglyTyped<TInnerType>
    {
        public static bool operator ==(StronglyTyped<TInnerType> objA, StronglyTyped<TInnerType> objB)
        {
            return object.Equals(objA, objB);
        }

        public static bool operator !=(StronglyTyped<TInnerType> objA, StronglyTyped<TInnerType> objB)
        {
            return !object.Equals(objA, objB);
        }

        public static bool operator ==(object objA, StronglyTyped<TInnerType> objB)
        {
            return object.Equals(objA, objB);
        }

        public static bool operator !=(object objA, StronglyTyped<TInnerType> objB)
        {
            return !object.Equals(objA, objB);
        }

        public static bool operator ==(StronglyTyped<TInnerType> objA, object objB)
        {
            return object.Equals(objA, objB);
        }

        public static bool operator !=(StronglyTyped<TInnerType> objA, object objB)
        {
            return !object.Equals(objA, objB);
        }

        public static implicit operator TInnerType?(StronglyTyped<TInnerType>? value)
        {
            return value is not null
                ? value.Value
                : default;
        }
    }
}
