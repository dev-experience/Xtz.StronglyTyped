``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1052 (20H2/October2020Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=5.0.301
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT


```
|                             Method |      Mean |    Error |   StdDev | Ratio | RatioSD |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|----------------------------------- |----------:|---------:|---------:|------:|--------:|--------:|-------:|------:|----------:|
|                             string |  27.88 μs | 0.078 μs | 0.073 μs |  1.00 |    0.00 |  2.3804 | 0.2747 |     - |     39 KB |
|              StronglyTyped&lt;string&gt; | 157.08 μs | 2.193 μs | 2.051 μs |  5.63 |    0.08 |  4.6387 | 0.4883 |     - |     78 KB |
|   StronglyTyped&lt;ValueType&lt;string&gt;&gt; | 156.69 μs | 1.911 μs | 1.694 μs |  5.62 |    0.06 |  7.5684 | 0.7324 |     - |    125 KB |
|         StronglyTyped&lt;MailAddress&gt; | 462.55 μs | 3.447 μs | 3.055 μs | 16.59 |    0.12 | 26.8555 | 2.9297 |     - |    445 KB |
| &#39;Other StronglyTyped&lt;MailAddress&gt;&#39; | 462.55 μs | 3.117 μs | 2.603 μs | 16.58 |    0.09 | 26.8555 | 2.9297 |     - |    445 KB |
