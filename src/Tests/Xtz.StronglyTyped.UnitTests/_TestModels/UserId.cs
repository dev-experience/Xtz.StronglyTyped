using System;
using System.ComponentModel;
using Xtz.StronglyTyped.TypeConverters;

namespace Xtz.StronglyTyped.UnitTests
{
    // TODO: Replace by auto-generated class with [StrongType(typeof(int))]
    [TypeConverter(typeof(TypeConverter<UserId, Guid>))]
    public class UserId : StronglyTyped<Guid>
    {
        public UserId(Guid value)
            : base(value)
        {
        }

        protected override bool IsValid(Guid value)
        {
            return value != Guid.Empty;
        }
    }
}
