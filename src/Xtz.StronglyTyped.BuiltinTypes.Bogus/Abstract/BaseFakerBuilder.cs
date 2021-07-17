using System;
using Bogus;
using Microsoft.Extensions.Caching.Memory;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public abstract class BaseFakerBuilder
    {
        private static readonly MemoryCacheEntryOptions MEMORY_CACHE_ENTRY_OPTIONS = new() { Size = 1 };

        private static readonly MemoryCache CACHE = new(new MemoryCacheOptions { SizeLimit = 500 });

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

            var cachedValue = CACHE.Get(key);
            if (cachedValue != null)
            {
                return (cachedValue as Faker<TValue>)!;
            }

            var result = fakerFactory();
            CACHE.Set(key, result, MEMORY_CACHE_ENTRY_OPTIONS);
            return result;
        }
    }
}