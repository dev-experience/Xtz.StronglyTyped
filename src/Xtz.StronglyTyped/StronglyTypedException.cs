using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Xtz.StronglyTyped
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class StronglyTypedException : Exception
    {
        public Type Type { get; }

        public StronglyTypedException(Type type, string errorMessage)
            : base(errorMessage)
        {
            Type = type;
        }

        public StronglyTypedException(Type type, string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
            Type = type;
        }

        /// <summary>
        /// Constructor is used for deserialization.
        /// </summary>
        protected StronglyTypedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Type = (Type)info.GetValue(nameof(Type), typeof(Type));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Type), Type);
        }
    }
}
