using System;
using System.Linq;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Bogus;
using Xtz.StronglyTyped.Benchmark.Models;

namespace Xtz.StronglyTyped.Benchmark
{
    [MemoryDiagnoser]
    public class SystemTextJsonSerializationGuidIds
    {
        private readonly EmployeeGuidId[] _employeeGuidIds;

        private readonly EmployeeGuidId[] _otherEmployeeGuidIds;

        private readonly Guid[] _guids;

        private readonly GuidStructId[] _stronglyTypedGuidStructs;

        public SystemTextJsonSerializationGuidIds()
        {
            var faker = new Faker<EmployeeGuidId>();

            _employeeGuidIds = faker.Generate(Program.VALUE_COUNT).ToArray();
            _otherEmployeeGuidIds = faker.Generate(Program.VALUE_COUNT).ToArray();
            _guids = _employeeGuidIds.Select(x => x.Value).ToArray();
            _stronglyTypedGuidStructs = _employeeGuidIds.Select(x => (GuidStructId)x.Value).ToArray();
        }

        [Benchmark(Baseline = true, Description = "Guid")]
        public string SerializeEmailStrings()
        {
            var result = JsonSerializer.Serialize(_guids);
            return result;
        }

        [Benchmark(Description = "StronglyTyped<ValueType<Guid>>")]
        public string SerializeStronglyTypedGuidStructs()
        {
            var result = JsonSerializer.Serialize(_stronglyTypedGuidStructs);
            return result;
        }

        [Benchmark(Description = "StronglyTyped<Guid>")]
        public string SerializeStronglyTypedGuids()
        {
            var result = JsonSerializer.Serialize(_employeeGuidIds);
            return result;
        }

        [Benchmark(Description = "Other StronglyTyped<Guid>")]
        public string SerializeOtherStronglyTypedGuids()
        {
            var result = JsonSerializer.Serialize(_otherEmployeeGuidIds);
            return result;
        }
    }
}