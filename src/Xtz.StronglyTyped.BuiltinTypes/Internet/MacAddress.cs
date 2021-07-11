using System;
using System.Net.NetworkInformation;
using Xtz.StronglyTyped.SourceGenerator;

namespace Xtz.StronglyTyped.BuiltinTypes.Internet
{
    /// <summary>
    /// MAC address.
    /// </summary>
    [StrongType(typeof(PhysicalAddress))]
    public partial class MacAddress
    {
        public MacAddress(byte[] value)
            : base(new PhysicalAddress(value))
        {
        }

        public override string ToString()
        {
            return ToString(Separator.Hyphen);
        }

        public string ToString(Separator? separator)
        {
            var stringValue = Value.ToString();
            var string12 = stringValue.PadRight(12, '0');
            if (separator == Separator.None) return string12;

            string glue;
            switch (separator)
            {
                case null:
                    return string12;
                case Separator.Dot:
                    glue = ".";
                    return string.Format(
                        "{1}{0}{2}{0}{3}",
                        glue,
                        string12[0..4],
                        string12[4..8],
                        string12[8..12]);
                case Separator.Colon:
                    glue = ":";
                    break;
                case Separator.Hyphen:
                    glue = "-";
                    break;
                default:
                    throw new ArgumentException($"Incorrect separator value '{separator}'", nameof(separator));
            }

            var result = string.Format(
                "{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                glue,
                string12[0..2],
                string12[2..4],
                string12[4..6],
                string12[6..8],
                string12[8..10],
                string12[10..12]);
            return result;
        }

        public enum Separator
        {
            None,
            Hyphen,
            Colon,
            Dot,
        }
    }
}
