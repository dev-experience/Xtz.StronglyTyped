using System;
using System.Diagnostics.CodeAnalysis;

namespace Xtz.StronglyTyped.SourceGenerator
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class StrongTypeAttribute : Attribute
    {
        public Type InnerType { get; }

        public Allow Allow { get; }

        public StrongTypeAttribute(Type innerType, Allow allow = Allow.None)
        {
            InnerType = innerType;
            Allow = allow;
        }

        public StrongTypeAttribute(Allow allow = Allow.None)
            : this(typeof(string), allow)
        {
        }
    }
}
