using System.ComponentModel;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.UnitTests
{
    [TypeConverter(typeof(TypeConverter<UserIntId, int>))]
    public struct UserIntId : IStronglyTyped<int>
    {
        public int Value { get; }

        public UserIntId(int value)
        {
            Value = value;
            ThrowIfInvalid(value);
        }

        private void ThrowIfInvalid(int value)
        {
            // ID must be greater than 0
            if (value <= 0)
            {
                throw new StronglyTypedException(GetType(), $"'{value}' value is invalid");
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static explicit operator UserIntId(int value)
        {
            return new UserIntId(value);
        }

        public static implicit operator int(UserIntId stronglyTyped)
        {
            return stronglyTyped.Value;
        }
    }
}
