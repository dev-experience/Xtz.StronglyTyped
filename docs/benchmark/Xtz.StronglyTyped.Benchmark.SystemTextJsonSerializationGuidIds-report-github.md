``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1052 (20H2/October2020Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=5.0.301
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT


```
|                         Method |      Mean |    Error |   StdDev | Ratio | RatioSD |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |----------:|---------:|---------:|------:|--------:|--------:|-------:|------:|----------:|
|                           Guid |  32.91 μs | 0.671 μs | 0.773 μs |  1.00 |    0.00 |  4.6387 | 0.9155 |     - |     76 KB |
| StronglyTyped&lt;ValueType&lt;Guid&gt;&gt; | 231.15 μs | 0.558 μs | 0.495 μs |  7.05 |    0.20 | 16.6016 | 3.1738 |     - |    272 KB |
|            StronglyTyped&lt;Guid&gt; | 254.04 μs | 0.835 μs | 0.740 μs |  7.74 |    0.24 | 12.6953 | 2.4414 |     - |    209 KB |
|    &#39;Other StronglyTyped&lt;Guid&gt;&#39; | 253.07 μs | 0.825 μs | 0.731 μs |  7.71 |    0.22 | 12.6953 | 2.4414 |     - |    209 KB |
