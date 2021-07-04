using System;

namespace Xtz.StronglyTyped.BuiltinTypes.Ids
{
    /// <summary>
    /// Base class for GUID-based IDs.
    /// </summary>
    public abstract class GuidId : StronglyTyped<Guid>
    {
        protected GuidId(Guid value)
            : base(value)
        {
        }

        protected GuidId()
            : base(Guid.NewGuid())
        {
        }
    }
}