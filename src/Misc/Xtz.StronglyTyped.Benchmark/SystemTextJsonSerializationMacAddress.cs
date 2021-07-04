using System.Linq;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Bogus;
using Xtz.StronglyTyped.Benchmark.Models;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.Benchmark
{
    [MemoryDiagnoser]
    public class SystemTextJsonSerializationMacAddress
    {
        private readonly InternetFakerBuilder _fakerBuilder;

        private readonly Faker<MacAddress> _faker;

        private readonly MacAddress[] _macAddresses;

        private readonly MacAddress[] _otherMacAddresses;

        private readonly string[] _strings;

        private readonly StronglyTypedString[] _stronglyTypedStrings;

        private readonly StronglyTypedStringStruct[] _stronglyTypedStructs;

        public SystemTextJsonSerializationMacAddress()
        {
            _fakerBuilder = new InternetFakerBuilder(true);
            _faker = _fakerBuilder.BuildMacAddressFaker();

            _macAddresses = _faker.Generate(Program.VALUE_COUNT).ToArray();
            _otherMacAddresses = _faker.Generate(Program.VALUE_COUNT).ToArray();
            _strings = _macAddresses.Select(x => x.ToString()).ToArray();
            _stronglyTypedStrings = _strings.Select(x => (StronglyTypedString)x).ToArray();
            _stronglyTypedStructs = _strings.Select(x => (StronglyTypedStringStruct)x).ToArray();

            ////_serializationOptions = new JsonSerializerOptionsFactory().Create();
        }

        [Benchmark(Baseline = true, Description = "string")]
        public string SerializeEmailStrings()
        {
            var result = JsonSerializer.Serialize(_strings);
            return result;
        }

        [Benchmark(Description = "StronglyTyped<string>")]
        public string SerializeStronglyTypedStrings()
        {
            var result = JsonSerializer.Serialize(_stronglyTypedStrings);
            return result;
        }

        [Benchmark(Description = "StronglyTyped<ValueType<string>>")]
        public string SerializeStronglyTypedStringStructs()
        {
            var result = JsonSerializer.Serialize(_stronglyTypedStructs);
            return result;
        }

        [Benchmark(Description = "StronglyTyped<MailAddress>")]
        public string SerializeStronglyTypedEmails()
        {
            var result = JsonSerializer.Serialize(_macAddresses);
            return result;
        }

        [Benchmark(Description = "Other StronglyTyped<MailAddress>")]
        public string SerializeOtherStronglyTypedEmails()
        {
            var result = JsonSerializer.Serialize(_otherMacAddresses);
            return result;
        }
    }
}