using System.ComponentModel;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.UnitTests
{
    // TODO: Replace by auto-generated struct with [StrongType(typeof(int))]
    [TypeConverter(typeof(TypeConverter<EmployeeIntId, int>))]
    public struct EmployeeIntId : IStronglyTyped<int>
    {
        public int Value { get; }

        public EmployeeIntId(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static explicit operator EmployeeIntId(int value)
        {
            return new EmployeeIntId(value);
        }

        public static implicit operator int(EmployeeIntId stronglyTyped)
        {
            return stronglyTyped.Value;
        }
    }
}
