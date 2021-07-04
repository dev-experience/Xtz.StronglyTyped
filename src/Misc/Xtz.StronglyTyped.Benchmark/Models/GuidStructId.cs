using System;

namespace Xtz.StronglyTyped.Benchmark.Models
{
    [StrongType(typeof(Guid))]
    public partial struct GuidStructId
    {
        ////TODO: Support overriding ToString()
        //public override string ToString()
        //{
        //    return $"{Value:D}";
        //}

        public GuidStructId(string value)
            : this(Guid.Parse(value))
        {
        }

        // TODO: Move to the struct generation
        public static explicit operator GuidStructId(string value)
        {
            return new GuidStructId(value);
        }

        // TODO: Move to the struct generation
        public static implicit operator string(GuidStructId stronglyTyped)
        {
            return stronglyTyped.ToString();
        }
    }
}