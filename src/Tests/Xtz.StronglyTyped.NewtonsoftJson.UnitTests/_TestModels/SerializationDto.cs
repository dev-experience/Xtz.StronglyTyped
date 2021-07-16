using System;
using System.Diagnostics;

namespace Xtz.StronglyTyped.NewtonsoftJson.UnitTests
{
    [DebuggerDisplay("{TestValue,nq}")]
    public class SerializationDto<TStronglyTyped>
        where TStronglyTyped : IStronglyTyped
    {
        public SerializationDto()
        {
        }

        public SerializationDto(TStronglyTyped stronglyTyped)
        {
            TestValue = stronglyTyped;
        }

        public SerializationDto(object value)
        {
            TestValue = (TStronglyTyped)Activator.CreateInstance(typeof(TStronglyTyped), value);
        }

        public TStronglyTyped TestValue { get; set; }
    }
}