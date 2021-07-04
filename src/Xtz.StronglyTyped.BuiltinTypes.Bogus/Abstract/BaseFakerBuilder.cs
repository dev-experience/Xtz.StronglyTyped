using System;
using Bogus;
using Microsoft.Extensions.Caching.Memory;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public abstract class BaseFakerBuilder
    {
        private static readonly MemoryCacheEntryOptions MemoryCacheEntryOptions = new() { Size = 1 };

        private static readonly MemoryCache Cache = new(new MemoryCacheOptions { SizeLimit = 500 });

        private readonly bool _useFakerCache;

        /// <param name="useFakerCache">Do store created faker objects in in-memory cache</param>
        protected BaseFakerBuilder(bool useFakerCache)
        {
            _useFakerCache = useFakerCache;
        }

        public Faker<TValue> GetFaker<TValue>(Func<Faker<TValue>> fakerFactory, string? cacheKey = null)
            where TValue : class
        {
            if (!(_useFakerCache))
            {
                return fakerFactory();
            }

            var key = !string.IsNullOrEmpty(cacheKey)
                ? $"{typeof(TValue).FullName}_{cacheKey}"
                : typeof(TValue).FullName;

            var cachedValue = Cache.Get(key);
            if (cachedValue != null)
            {
                return (cachedValue as Faker<TValue>)!;
            }

            var result = fakerFactory();
            Cache.Set(key, result, MemoryCacheEntryOptions);
            return result;
        }
    }
}