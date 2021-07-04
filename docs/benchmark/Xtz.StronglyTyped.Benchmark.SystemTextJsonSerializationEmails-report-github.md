``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1052 (20H2/October2020Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=5.0.301
  [Host]     : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT


```
|                             Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|----------------------------------- |----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                             string |  32.09 μs | 0.344 μs | 0.268 μs |  1.00 |    0.00 | 3.1433 | 0.5188 |     - |     52 KB |
|              StronglyTyped&lt;string&gt; | 166.77 μs | 0.776 μs | 0.726 μs |  5.19 |    0.06 | 5.3711 | 0.7324 |     - |     90 KB |
|   StronglyTyped&lt;ValueType&lt;string&gt;&gt; | 176.85 μs | 0.358 μs | 0.299 μs |  5.51 |    0.05 | 8.3008 | 0.9766 |     - |    137 KB |
|         StronglyTyped&lt;MailAddress&gt; | 188.69 μs | 0.798 μs | 0.747 μs |  5.88 |    0.05 | 9.5215 | 1.2207 |     - |    159 KB |
| &#39;Other StronglyTyped&lt;MailAddress&gt;&#39; | 191.47 μs | 1.077 μs | 1.008 μs |  5.97 |    0.05 | 9.5215 | 1.2207 |     - |    159 KB |
