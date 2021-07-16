using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Xtz.StronglyTyped.Benchmark
{
    public class Program
    {
        public const int VALUE_COUNT = 1000;

        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance;

#if DEBUG
            config = new DebugBuildConfig();
#endif

            BenchmarkRunner.Run<SystemTextJsonSerializationEmails>(config);
            BenchmarkRunner.Run<SystemTextJsonSerializationMacAddress>(config);
            BenchmarkRunner.Run<SystemTextJsonSerializationGuidIds>(config);
            BenchmarkRunner.Run<SystemTextJsonSerializationIntIds>(config);
        }
    }
}