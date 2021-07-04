using System;

namespace Xtz.StronglyTyped
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class StrongTypeAttribute : Attribute
    {
        public Type InnerType { get; }

        public StrongTypeAttribute()
        {
            InnerType = typeof(string);
        }

        public StrongTypeAttribute(Type innerType)
        {
            InnerType = innerType;
        }
    }
}
