using System;
using System.ComponentModel;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.UnitTests
{
    // TODO: Replace by auto-generated class with [StrongType(typeof(int))]
    [TypeConverter(typeof(TypeConverter<CountryId, int>))]
    public class CountryId : StronglyTyped<int>, IConvertible
    {
        public CountryId(int value)
            : base(value)
        {
        }

        public TypeCode GetTypeCode()
        {
            return Value.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(Boolean)}'");
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(Byte)}'");
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(Char)}'");
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(DateTime)}'");
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(Decimal)}'");
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(Double)}'");
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(Int16)}'");
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Value;
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Value;
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(SByte)}'");
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(Single)}'");
        }

        public string ToString(IFormatProvider provider)
        {
            return Value.ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{conversionType}'");
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(UInt16)}'");
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(UInt32)}'");
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException($"'{nameof(CountryId)}' is not convertible to '{nameof(UInt64)}'");
        }

        protected override bool IsValid(int value)
        {
            // ID must be greater than 0
            return value > 0;
        }
    }
}
