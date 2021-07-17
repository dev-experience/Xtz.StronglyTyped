using System.Linq;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Xtz.StronglyTyped.Benchmark.Models;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.Benchmark
{
    [MemoryDiagnoser]
    public class SystemTextJsonSerializationMacAddress
    {
        private readonly MacAddress[] _macAddresses;

        private readonly MacAddress[] _otherMacAddresses;

        private readonly string[] _strings;

        private readonly StronglyTypedString[] _stronglyTypedStrings;

        private readonly StronglyTypedStringStruct[] _stronglyTypedStructs;

        public SystemTextJsonSerializationMacAddress()
        {
            var fakerBuilder = new InternetFakerBuilder();
            var faker = fakerBuilder.BuildMacAddressFaker();

            _macAddresses = faker.Generate(Program.VALUE_COUNT).ToArray();
            _otherMacAddresses = faker.Generate(Program.VALUE_COUNT).ToArray();
            _strings = _macAddresses.Select(x => x.ToString()).ToArray();
            _stronglyTypedStrings = _strings.Select(x => (StronglyTypedString)x).ToArray();
            _stronglyTypedStructs = _strings.Select(x => (StronglyTypedStringStruct)x).ToArray();

            ////_serializationOptions = new JsonSerializerOptionsFactory().Create();
        }

        [Benchmark(Baseline = true, Description = "string")]
        public string SerializeMacAddressStrings()
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

        [Benchmark(Description = "StronglyTyped<PhysicalAddress>")]
        public string SerializeStronglyTypedMacAddresses()
        {
            var result = JsonSerializer.Serialize(_macAddresses);
            return result;
        }

        [Benchmark(Description = "Other StronglyTyped<PhysicalAddress>")]
        public string SerializeOtherStronglyTypedMacAddresses()
        {
            var result = JsonSerializer.Serialize(_otherMacAddresses);
            return result;
        }
    }
}