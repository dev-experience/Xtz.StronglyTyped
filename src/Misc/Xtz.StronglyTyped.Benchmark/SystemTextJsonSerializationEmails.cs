using System.Linq;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Xtz.StronglyTyped.Benchmark.Models;
using Xtz.StronglyTyped.BuiltinTypes.Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Internet;

namespace Xtz.StronglyTyped.Benchmark
{
    [MemoryDiagnoser]
    public class SystemTextJsonSerializationEmails
    {
        private readonly Email[] _emails;

        private readonly Email[] _otherEmails;

        private readonly string[] _strings;

        private readonly StronglyTypedString[] _stronglyTypedStrings;

        private readonly StronglyTypedStringStruct[] _stronglyTypedStructs;

        public SystemTextJsonSerializationEmails()
        {
            ////System.Diagnostics.Debugger.Launch();

            var fakerBuilder = new InternetFakerBuilder(true);
            var faker = fakerBuilder.BuildEmailFaker();

            _emails = faker.Generate(Program.VALUE_COUNT).ToArray();
            _otherEmails = faker.Generate(Program.VALUE_COUNT).ToArray();
            _strings = _emails.Select(x => x.ToString()).ToArray();
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
            var result = JsonSerializer.Serialize(_emails);
            return result;
        }

        [Benchmark(Description = "Other StronglyTyped<MailAddress>")]
        public string SerializeOtherStronglyTypedEmails()
        {
            var result = JsonSerializer.Serialize(_otherEmails);
            return result;
        }
    }
}