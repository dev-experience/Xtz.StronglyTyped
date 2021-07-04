using System;

namespace Xtz.StronglyTyped
{
    public abstract partial class StronglyTyped<TInnerType>
    {
        public static bool operator ==(StronglyTyped<TInnerType> objA, StronglyTyped<TInnerType> objB)
        {
            return Object.Equals(objA, objB);
        }

        public static bool operator !=(StronglyTyped<TInnerType> objA, StronglyTyped<TInnerType> objB)
        {
            return !Object.Equals(objA, objB);
        }

        public static bool operator ==(object objA, StronglyTyped<TInnerType> objB)
        {
            return Object.Equals(objA, objB);
        }

        public static bool operator !=(object objA, StronglyTyped<TInnerType> objB)
        {
            return !Object.Equals(objA, objB);
        }

        public static bool operator ==(StronglyTyped<TInnerType> objA, object objB)
        {
            return Object.Equals(objA, objB);
        }

        public static bool operator !=(StronglyTyped<TInnerType> objA, object objB)
        {
            return !Object.Equals(objA, objB);
        }

        public static implicit operator TInnerType?(StronglyTyped<TInnerType>? value)
        {
            return value is not null
                ? value.Value
                : default;
        }
    }
}
