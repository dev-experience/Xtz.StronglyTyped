``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1052 (20H2/October2020Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=5.0.301
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT


```
|                        Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------ |----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                           int |  16.22 μs | 0.094 μs | 0.088 μs |  1.00 |    0.00 | 1.2512 | 0.0610 |     - |     21 KB |
| StronglyTyped&lt;ValueType&lt;int&gt;&gt; | 131.11 μs | 0.455 μs | 0.380 μs |  8.08 |    0.04 | 4.8828 | 0.2441 |     - |     83 KB |
|            StronglyTyped&lt;int&gt; | 155.37 μs | 0.708 μs | 0.628 μs |  9.57 |    0.06 | 3.4180 | 0.2441 |     - |     60 KB |
|    &#39;Other StronglyTyped&lt;int&gt;&#39; | 155.96 μs | 0.452 μs | 0.401 μs |  9.61 |    0.04 | 3.4180 | 0.2441 |     - |     60 KB |
