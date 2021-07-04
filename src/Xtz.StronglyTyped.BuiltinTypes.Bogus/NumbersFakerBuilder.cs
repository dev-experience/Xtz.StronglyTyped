using System;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Numbers;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class NumbersFakerBuilder : BaseFakerBuilder
    {
        public NumbersFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// A random even <see cref="Int32"/> faker.
        /// </summary>
        /// <param name="min">Lower bound, inclusive. Default: 0</param>
        /// <param name="max">Upper bound, inclusive. Default: 1</param>
        /// <exception cref="ArgumentException">Thrown if it is impossible to select an even number satisfying the specified range.</exception>
        public Faker<EvenInt32> BuildEvenInt32Faker(int min = 0, int max = 1)
        {
            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<EvenInt32>()
                .CustomInstantiator(f => new EvenInt32(f.Random.Even(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random even <see cref="Int64"/> faker.
        /// </summary>
        /// <remarks>Generates values only within <see cref="Int32"/> range.</remarks>
        /// <param name="min">Lower bound, inclusive. Default: 0</param>
        /// <param name="max">Upper bound, inclusive. Default: 1</param>
        /// <exception cref="ArgumentException">Thrown if it is impossible to select an even number satisfying the specified range.</exception>
        public Faker<EvenInt64> BuildEvenInt64Faker(int min = 0, int max = 1)
        {
            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<EvenInt64>()
                .CustomInstantiator(f => new EvenInt64(f.Random.Even(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random negative <see cref="Int32"/> faker.
        /// </summary>
        /// <param name="min">Min value. Default: <see cref="Int32.MinValue"/></param>
        /// <param name="max">Max value, Default: -1</param>
        /// <exception cref="ArgumentException">Thrown if range is invalid.</exception>
        public Faker<NegativeInt32> BuildNegativeInt32Faker(int min = int.MinValue, int max = -1)
        {
            max = Math.Min(max, -1);
            if (min > max) throw new ArgumentException($"The min/max range is invalid. The minimum value '{min}' is greater than the maximum value '{max}'");

            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<NegativeInt32>()
                .CustomInstantiator(f => new NegativeInt32(f.Random.Int(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random negative <see cref="Int64"/> faker.
        /// </summary>
        /// <remarks>Generates values only within <see cref="Int32"/> range.</remarks>
        /// <param name="min">Min value. Default: <see cref="Int32.MinValue"/></param>
        /// <param name="max">Max value, Default: -1</param>
        /// <exception cref="ArgumentException">Thrown if range is invalid.</exception>
        public Faker<NegativeInt64> BuildNegativeInt64Faker(int min = int.MinValue, int max = -1)
        {
            max = Math.Min(max, -1);
            if (min > max) throw new ArgumentException($"The min/max range is invalid. The minimum value '{min}' is greater than the maximum value '{max}'");

            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<NegativeInt64>()
                .CustomInstantiator(f => new NegativeInt64(f.Random.Int(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random non-negative <see cref="Int32"/> faker.
        /// </summary>
        /// <param name="min">Min value. Default: 0</param>
        /// <param name="max">Max value, Default: <see cref="Int32.MaxValue"/></param>
        public Faker<NonNegativeInt32> BuildNonNegativeInt32Faker(int min = 0, int max = int.MaxValue)
        {
            min = Math.Max(min, 0);
            if (min > max) throw new ArgumentException($"The min/max range is invalid. The minimum value '{min}' is greater than the maximum value '{max}'");

            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<NonNegativeInt32>()
                .CustomInstantiator(f => new NonNegativeInt32(f.Random.Int(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random non-negative <see cref="Int64"/> faker.
        /// </summary>
        /// <remarks>Generates values only within <see cref="Int32"/> range.</remarks>
        /// <param name="min">Min value. Default: 0</param>
        /// <param name="max">Max value, Default: <see cref="Int32.MaxValue"/></param>
        public Faker<NonNegativeInt64> BuildNonNegativeInt64Faker(int min = 0, int max = int.MaxValue)
        {
            min = Math.Max(min, 0);
            if (min > max) throw new ArgumentException($"The min/max range is invalid. The minimum value '{min}' is greater than the maximum value '{max}'");

            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<NonNegativeInt64>()
                .CustomInstantiator(f => new NonNegativeInt64(f.Random.Int(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random non-positive <see cref="Int32"/> faker.
        /// </summary>
        /// 
        /// <param name="min">Min value. Default: <see cref="Int32.MinValue"/></param>
        /// <param name="max">Max value, Default: 0</param>
        public Faker<NonPositiveInt32> BuildNonPositiveInt32Faker(int min = int.MinValue, int max = 0)
        {
            max = Math.Min(max, 0);
            if (min > max) throw new ArgumentException($"The min/max range is invalid. The minimum value '{min}' is greater than the maximum value '{max}'");

            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<NonPositiveInt32>()
                .CustomInstantiator(f => new NonPositiveInt32(f.Random.Int(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random non-positive <see cref="Int64"/> faker.
        /// </summary>
        /// 
        /// <remarks>Generates values only within <see cref="Int32"/> range.</remarks>
        /// <param name="min">Min value. Default: <see cref="Int32.MinValue"/></param>
        /// <param name="max">Max value, Default: 0</param>
        public Faker<NonPositiveInt64> BuildNonPositiveInt64Faker(int min = int.MinValue, int max = 0)
        {
            max = Math.Min(max, 0);
            if (min > max) throw new ArgumentException($"The min/max range is invalid. The minimum value '{min}' is greater than the maximum value '{max}'");

            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<NonPositiveInt64>()
                .CustomInstantiator(f => new NonPositiveInt64(f.Random.Int(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random odd <see cref="Int32"/> faker.
        /// </summary>
        /// <param name="min">Lower bound, inclusive. Default: 0</param>
        /// <param name="max">Upper bound, inclusive. Default: 1</param>
        /// <exception cref="ArgumentException">Thrown if it is impossible to select an odd number satisfying the specified range.</exception>
        public Faker<OddInt32> BuildOddInt32Faker(int min = 0, int max = 1)
        {
            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<OddInt32>()
                .CustomInstantiator(f => new OddInt32(f.Random.Odd(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random odd <see cref="Int64"/> faker.
        /// </summary>
        /// <remarks>Generates values only within <see cref="Int32"/> range.</remarks>
        /// <param name="min">Lower bound, inclusive. Default: 0</param>
        /// <param name="max">Upper bound, inclusive. Default: 1</param>
        /// <exception cref="ArgumentException">Thrown if it is impossible to select an odd number satisfying the specified range.</exception>
        public Faker<OddInt64> BuildOddInt64Faker(int min = 0, int max = 1)
        {
            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<OddInt64>()
                .CustomInstantiator(f => new OddInt64(f.Random.Odd(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random positive <see cref="Int32"/> faker.
        /// </summary>
        /// <param name="min">Min value. Default: 0</param>
        /// <param name="max">Max value, Default: <see cref="Int32.MaxValue"/></param>
        public Faker<PositiveInt32> BuildPositiveInt32Faker(int min = 1, int max = int.MaxValue)
        {
            min = Math.Max(min, 1);
            if (min > max) throw new ArgumentException($"The min/max range is invalid. The minimum value '{min}' is greater than the maximum value '{max}'");

            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<PositiveInt32>()
                .CustomInstantiator(f => new PositiveInt32(f.Random.Int(min, max))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random positive <see cref="Int64"/> faker.
        /// </summary>
        /// <remarks>Generates values only within <see cref="Int32"/> range.</remarks>
        /// <param name="min">Min value. Default: 0</param>
        /// <param name="max">Max value, Default: <see cref="Int32.MaxValue"/>.</param>
        public Faker<PositiveInt64> BuildPositiveInt64Faker(int min = 1, int max = int.MaxValue)
        {
            min = Math.Max(min, 1);
            if (min > max) throw new ArgumentException($"The min/max range is invalid. The minimum value '{min}' is greater than the maximum value '{max}'");

            var cacheKey = $"{min}|{max}";

            var result = GetFaker(() => new Faker<PositiveInt64>()
                .CustomInstantiator(f => new PositiveInt64(f.Random.Int(min, max))), cacheKey);
            return result;
        }
    }
}