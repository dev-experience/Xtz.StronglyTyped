using System;

namespace Xtz.StronglyTyped.SourceGenerator
{
    /// <summary>
    /// Flags to configure strong types generation.
    /// </summary>
    [Flags]
    public enum Allow
    {
        None = 0,
        /// <summary>
        /// Allow empty strings or default struct values (but doesn't affect numbers).
        /// </summary>
        /// <remarks>
        /// <para><b>Difference</b> for <see langword="struct"/>:</para>
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// <para>
        /// <c>[<see cref="StrongTypeAttribute"/>(<see langword="typeof"/>(<see cref="Guid"/>))]</c><br />
        /// <b>ERROR</b>: 00000000-0000-0000-0000-000000000000
        /// </para>
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <para>
        /// <c>[<see cref="StrongTypeAttribute"/>(<see langword="typeof"/>(<see cref="Guid"/>), Allow.Empty)]</c><br />
        /// <b>OK</b>: 00000000-0000-0000-0000-000000000000
        /// </para>
        /// </description>
        /// </item>
        /// </list>
        /// <para><b>No difference</b> for <see cref="int"/> (and any other number type)</para>
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// <para>
        /// <c>[<see cref="StrongTypeAttribute"/>(<see langword="typeof"/>(<see cref="int"/>))]</c><br />
        /// <b>OK</b>: 0
        /// </para>
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <para>
        /// <c>[<see cref="StrongTypeAttribute"/>(<see langword="typeof"/>(<see cref="int"/>), Allow.Empty)]</c><br />
        /// <b>OK</b>: 0
        /// </para>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        Empty = 1,
    }
}
