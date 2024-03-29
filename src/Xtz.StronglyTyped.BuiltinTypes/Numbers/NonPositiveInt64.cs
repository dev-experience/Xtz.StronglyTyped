﻿using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Numbers
{
    /// <summary>
    /// Non-positive (equal or less than zero) <see cref="System.Int64"/> number.
    /// </summary>
    [StrongType(typeof(long), Allow.Empty)]
    public partial class NonPositiveInt64
    {
        protected override bool IsValid(long value)
        {
            return value <= 0;
        }
    }
}