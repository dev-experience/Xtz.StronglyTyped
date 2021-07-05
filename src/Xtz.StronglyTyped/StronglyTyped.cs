using System.Diagnostics;

namespace Xtz.StronglyTyped
{
    /// <summary>
    /// Base strongly-typed class.
    /// </summary>
    /// <typeparam name="TInnerType">Inner type (can be a primitive or a more sophisticated one)</typeparam>
    [DebuggerDisplay("[class {GetType().Name,nq}] {Value,nq}")]
    public abstract partial class StronglyTyped<TInnerType> : IStronglyTyped<TInnerType>
        where TInnerType : notnull
    {
        // See more in `StronglyTyped.Operators.cs`

        public TInnerType Value { get; }

        protected StronglyTyped(TInnerType value)
        {
            Value = value;
            ThrowIfInvalid(value);
        }

        private void ThrowIfInvalid(TInnerType value)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (value == null)
            {
                Throw($"<null> value is invalid for type {GetType()}");
            }

            if (object.Equals(value, string.Empty))
            {
                Throw($"'' value is invalid for type {GetType()}");
            }

            if (!IsValid(value!))
            {
                Throw($"'{value}' value is invalid for type {GetType()}");
            }
        }

        protected void Throw(string errorMessage) => throw new StronglyTypedException(GetType(), errorMessage);

        protected virtual bool IsValid(TInnerType value)
        {
            // Bypass. Can be overriden
            return true;
        }

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (obj is not StronglyTyped<TInnerType> stronglyTyped)
            {
                return false;
            }

            return Equals(Value, stronglyTyped.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        // See more in `StronglyTyped.Operators.cs`
    }
}
