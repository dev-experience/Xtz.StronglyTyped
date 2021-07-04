using System;

namespace Xtz.StronglyTyped.TypeConverters
{
    public interface ICustomTypeConverter
    {
        Type StrongType { get; }

        Type InnerType { get; }
    }
}