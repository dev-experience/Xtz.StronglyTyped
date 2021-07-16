using System;
using System.Linq;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Bogus;
using Xtz.StronglyTyped.Benchmark.Models;

namespace Xtz.StronglyTyped.Benchmark
{
    [MemoryDiagnoser]
    public class SystemTextJsonSerializationIntIds
    {
        private readonly Faker<CompanyIntId> _faker;

        private readonly CompanyIntId[] _companyIntIds;

        private readonly CompanyIntId[] _otherCompanyIntIds;

        private readonly int[] _ints;

        private readonly IntStructId[] _stronglyTypedIntStructs;

        public SystemTextJsonSerializationIntIds()
        {
            _faker = new Faker<CompanyIntId>()
                .CustomInstantiator(f => new CompanyIntId(Math.Abs(f.Random.Int())));

            _companyIntIds = _faker.Generate(Program.VALUE_COUNT).ToArray();
            _otherCompanyIntIds = _faker.Generate(Program.VALUE_COUNT).ToArray();
            _ints = _companyIntIds.Select(x => x.Value).ToArray();
            _stronglyTypedIntStructs = _companyIntIds.Select(x => (IntStructId)x.Value).ToArray();
        }

        [Benchmark(Baseline = true, Description = "int")]
        public string SerializeInts()
        {
            var result = JsonSerializer.Serialize(_ints);
            return result;
        }

        [Benchmark(Description = "StronglyTyped<ValueType<int>>")]
        public string SerializeStronglyTypedIntStructs()
        {
            var result = JsonSerializer.Serialize(_stronglyTypedIntStructs);
            return result;
        }

        [Benchmark(Description = "StronglyTyped<int>")]
        public string SerializeStronglyTypedIntIds()
        {
            var result = JsonSerializer.Serialize(_companyIntIds);
            return result;
        }

        [Benchmark(Description = "Other StronglyTyped<int>")]
        public string SerializeOtherStronglyTypedIntIds()
        {
            var result = JsonSerializer.Serialize(_otherCompanyIntIds);
            return result;
        }
    }
}